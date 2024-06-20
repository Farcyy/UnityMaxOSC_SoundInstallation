// Import necessary namespaces
using UnityEngine;
using extOSC;

public class HeadTrackerControllInput : MonoBehaviour
{
    public OSCReceiver receiver; // Reference to the OSC receiver component
    public Transform playerCameraRoot; // Reference to the camera root object
    public Transform playerCapsule; // Reference to the capsule object
    private Quaternion lastRotation; // Variable to store the last received rotation

    public float sensitivity = 2.0f; // Sensitivity factor for rotation
    public float deadZone = 0.01f; // Dead zone threshold to ignore minor rotations

    // Start is called before the first frame update
    void Start()
    {
        // If the receiver is not set in the editor, try to get it from the GameObject
        if (receiver == null)
            receiver = GetComponent<OSCReceiver>();

        // Bind the method ApplyHeadRotation to the OSC address "/quaternion"
        receiver.Bind("/quaternion", ApplyHeadRotation);
    }

    // Method to apply the received head rotation
    void ApplyHeadRotation(OSCMessage message)
    {
        Quaternion rotation;
        // Try to get the quaternion data from the OSC message
        if (message.ToQuaternion(out rotation))
        {
            // Correct the rotation based on the received quaternion
            Quaternion correctedRotation = new Quaternion(rotation.z, -rotation.x, -rotation.y, -rotation.w);

            // Calculate the difference between the last and the current rotation
            Quaternion deltaRotation = Quaternion.Inverse(lastRotation) * correctedRotation;
            // Normalize and scale the rotation angles
            Vector3 deltaEulerAngles = NormalizeAngles(deltaRotation.eulerAngles) * sensitivity;

            // Apply dead zone to ignore small rotations
            if (Mathf.Abs(deltaEulerAngles.x) < deadZone) deltaEulerAngles.x = 0;
            if (Mathf.Abs(deltaEulerAngles.y) < deadZone) deltaEulerAngles.y = 0;
            if (Mathf.Abs(deltaEulerAngles.z) < deadZone) deltaEulerAngles.z = 0;

            // Update PlayerCameraRoot rotation for pitch and roll
            Vector3 cameraRootEuler = playerCameraRoot.localEulerAngles;
            cameraRootEuler.x += deltaEulerAngles.x; // Pitch
            cameraRootEuler.z += deltaEulerAngles.z; // Roll
            playerCameraRoot.localEulerAngles = cameraRootEuler;

            // Update PlayerCapsule rotation for yaw
            Vector3 capsuleEuler = playerCapsule.localEulerAngles;
            capsuleEuler.y += deltaEulerAngles.y; // Yaw
            playerCapsule.localEulerAngles = capsuleEuler;

            // Update the last rotation to the current corrected rotation
            lastRotation = correctedRotation;
        }
    }

    // Method to normalize angles to the range [-180, 180]
    Vector3 NormalizeAngles(Vector3 angles)
    {
        angles.x = NormalizeAngle(angles.x);
        angles.y = NormalizeAngle(angles.y);
        angles.z = NormalizeAngle(angles.z);
        return angles;
    }

    // Method to normalize a single angle to the range [-180, 180]
    float NormalizeAngle(float angle)
    {
        while (angle > 180)
            angle -= 360;
        while (angle < -180)
            angle += 360;
        return angle;
    }
}
