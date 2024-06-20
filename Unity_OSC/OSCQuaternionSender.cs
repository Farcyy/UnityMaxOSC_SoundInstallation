using UnityEngine;
using extOSC;

public class PlayerHeadPositionSender_OSC : MonoBehaviour
{
    public OSCTransmitter maxTransmitter;  // Transmitter to send data to Max
    public Transform playerCapsule;  // Reference to the PlayerCapsule object
    public Transform playerCameraRoot;  // Reference to the PlayerCameraRoot object

    void Start()
    {
        // Ensure that both playerCapsule and playerCameraRoot are assigned
        if (playerCapsule == null || playerCameraRoot == null)
        {
            Debug.LogError("PlayerCapsule or PlayerCameraRoot is not assigned!");
            return;
        }
    }

    void Update()
    {
        // Call the method to collect and send player head position data
        CollectPlayerHeadPosition();
    }

    void CollectPlayerHeadPosition()
    {
        // Get the rotation of the player capsule
        Quaternion capsuleRotation = playerCapsule.rotation;

        // Get the local rotation of the player camera root
        Quaternion cameraRootRotation = playerCameraRoot.localRotation;

        // Combine the rotations to get the overall head rotation
        Quaternion combinedRotation = capsuleRotation * cameraRootRotation;

        // Send the combined rotation as a quaternion
        SendQuaternionData(combinedRotation);
    }

    void SendQuaternionData(Quaternion quaternion)
    {
        // Create a new OSC message with the address "/quaternion"
        var message = new OSCMessage("/quaternion");

        // Add the quaternion values to the OSC message
        message.AddValue(OSCValue.Float(quaternion.x));
        message.AddValue(OSCValue.Float(quaternion.y));
        message.AddValue(OSCValue.Float(quaternion.z));
        message.AddValue(OSCValue.Float(quaternion.w));

        // Print the message content (optional for debugging)
        // Debug.Log($"Quaternion Values: x={quaternion.x}, y={quaternion.y}, z={quaternion.z}, w={quaternion.w}");

        // Send the message if the transmitter is assigned
        if (maxTransmitter != null)
        {
            maxTransmitter.Send(message);
        }
        else
        {
            Debug.LogWarning("Max Transmitter is not assigned!");
        }
    }
}
