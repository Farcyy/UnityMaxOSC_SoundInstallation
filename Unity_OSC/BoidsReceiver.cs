using UnityEngine;
using extOSC;

public class BoidsReceiver : MonoBehaviour
{
    // Public fields for configuration
    public GameObject player;              // The player GameObject
    public GameObject sphere;              // The sphere GameObject to be moved
    public OSCReceiver receiver;           // OSC receiver for incoming messages
    public float PositionMultiplier = 2;   // Multiplier for received position values

    public int pointIndex = 1;             // Index for the OSC point message

    public float smoothTime = 0.3f;        // Time for smoothing the movement of the sphere

    public bool PositionCorrectionY;       // Enable/disable Y-axis position correction

    // Private fields for internal state management
    private Vector3 offset;                // Offset position calculated from received OSC message
    private Vector3 targetPosition;        // Target position for the sphere
    private Vector3 velocity = Vector3.zero; // Velocity used in SmoothDamp

    void Start()
    {
        receiver = GetComponent<OSCReceiver>(); // Get the OSCReceiver component attached to the GameObject
        receiver.Bind($"/point/{pointIndex}/xyz", ReceivedMessage); // Bind the OSC message to the handler
    }

    // Method to handle received OSC messages
    private void ReceivedMessage(OSCMessage message)
    {
        // Extract the x, y, and z values from the OSC message
        float x = message.Values[0].FloatValue * PositionMultiplier;
        float z = message.Values[1].FloatValue * PositionMultiplier;
        float y = message.Values[2].FloatValue * PositionMultiplier;

        // Apply Y-axis position correction if enabled
        if (PositionCorrectionY && player != null)
        {
            y = Mathf.Max(y, 0);
        }

        // Calculate the offset position
        offset = new Vector3(x, y + 1, z);
    }

    void Update()
    {
        // Ensure both player and sphere are assigned
        if (player != null && sphere != null)
        {
            // Calculate the target position based on the player's position and the offset
            targetPosition = player.transform.position + offset;

            // Smoothly move the sphere towards the target position
            sphere.transform.position = Vector3.SmoothDamp(sphere.transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
