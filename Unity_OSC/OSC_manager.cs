// Import necessary namespaces
using UnityEngine;
using extOSC;

// Define a new namespace to encapsulate the OSC scripts
namespace OSCScripts
{
    // Define a class that inherits from MonoBehaviour, making it a Unity script component
    public class OSC_manager : MonoBehaviour
    {
        // Public variables that can be set from the Unity Editor
        public string ipAddress = "127.0.0.1"; // Default IP address for OSC transmitter
        public int port; // Port number for OSC transmitter
        public string address; // OSC address to send messages to

        private OSCTransmitter transmitter; // Reference to the OSC transmitter

        // Awake is called when the script instance is being loaded
        void Awake()
        {
            // Set up the OSC Transmitter
            transmitter = gameObject.AddComponent<OSCTransmitter>();
            transmitter.RemoteHost = ipAddress;
            transmitter.RemotePort = port;

            // Debug log to confirm transmitter setup
            Debug.Log($"OSC Transmitter initialized with IP: {ipAddress} and Port: {port}");
        }

        // Method to send distance data via OSC
        public void SendDistanceData(float distance)
        {
            // Debug log for sending distance data
            //Debug.Log($"OSCManager sending distance data: {distance}");

            if (transmitter != null)
            {
                // Create an OSC message with the specified address
                var message = new OSCMessage(address);
                // Add the distance value to the message
                message.AddValue(OSCValue.Float(distance));
                // Send the message using the transmitter
                transmitter.Send(message);

                // Debug log to confirm message sent
                //Debug.Log("OSC message sent by OSCManager.");
            }
            else
            {
                // Log an error if the transmitter is not set up
                Debug.LogError("OSC transmitter is not set up in OSCManager.");
            }
        }

        // Method to send pace data via OSC
        public void SendPaceData(float pace)
        {
            if (transmitter != null)
            {
                // Create an OSC message with the specified address
                var message = new OSCMessage(address);
                // Add the pace value to the message
                message.AddValue(OSCValue.Float(pace));
                // Send the message using the transmitter
                transmitter.Send(message);
            }
            else
            {
                // Log an error if the transmitter is not set up
                Debug.LogError("OSC transmitter is not set up in OSCManager.");
            }
        }

        // Method to send jump signal via OSC
        public void SendJumpSignal(bool jump)
        {
            if (transmitter != null)
            {
                // Create an OSC message with the specified address
                var message = new OSCMessage(address);
                // Add the jump signal value to the message
                message.AddValue(OSCValue.Bool(jump));
                // Send the message using the transmitter
                transmitter.Send(message);
            }
            else
            {
                // Log an error if the transmitter is not set up
                Debug.LogError("OSC transmitter is not set up in OSCManager.");
            }
        }
    }
}
