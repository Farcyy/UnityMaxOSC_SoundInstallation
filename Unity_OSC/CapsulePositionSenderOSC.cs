// Import necessary namespaces
using UnityEngine;  // Unity's core namespace for all engine-related functions
using extOSC;      // Namespace for the external Open Sound Control (OSC) library

// Define a new namespace to encapsulate the OSC scripts
namespace OSCScripts
{
    // Define a class that inherits from MonoBehaviour, making it a Unity script component
    public class CapsulePositionSender : MonoBehaviour
    {
        // Public variables that can be set from the Unity Editor
        public OSCTransmitter transmitter;  // The OSC transmitter responsible for sending messages
        public string address = "/player/xyz";  // The OSC address to which the message will be sent
        public GameObject player;  // Reference to the player GameObject whose position will be sent

        // Update is called once per frame
        void Update()
        {
            // Call the function to send the player's position
            SendPlayerPosition();
        }

        // Function to send the player's position via OSC
        void SendPlayerPosition()
        {
            // Check if both player and transmitter are assigned
            if (player != null && transmitter != null)
            {
                // Get the player's current position
                Vector3 position = player.transform.position;

                // Create a new OSC message with the specified address
                OSCMessage message = new OSCMessage(address);

                // Add the player's x, y, and z coordinates to the message as float values
                message.AddValue(OSCValue.Float(position.x));
                message.AddValue(OSCValue.Float(position.y));
                message.AddValue(OSCValue.Float(position.z));

                // Send the message using the transmitter
                transmitter.Send(message);
            }
        }
    }
}
