// Import necessary namespaces
using UnityEngine;

// Define a new namespace to encapsulate the OSC scripts
namespace OSCScripts
{
    // Define a class that inherits from MonoBehaviour, making it a Unity script component
    public class JumpObserver : MonoBehaviour
    {
        // Public variables that can be set from the Unity Editor
        public OSC_manager oscManager; // Reference to the OSC manager
        public CharacterController characterController; // Reference to the character controller

        private bool wasGrounded; // Variable to track if the character was grounded in the previous frame

        // Start is called before the first frame update
        void Start()
        {
            // Ensure the characterController reference is set, try to get it from the GameObject if not set
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }

            // Log an error if the oscManager reference is missing
            if (oscManager == null)
            {
                Debug.LogError("OSC_manager reference is missing!");
            }

            // Initialize wasGrounded with the current grounded state of the character
            wasGrounded = characterController.isGrounded;
        }

        // FixedUpdate is called at a fixed interval and is independent of the frame rate
        void FixedUpdate()
        {
            bool isGrounded = characterController.isGrounded; // Get the current grounded state of the character

            // Check if the player just jumped
            if (wasGrounded && !isGrounded)
            {
                // Send OSC signal indicating jump
                SendJumpSignal(true);
            }
            // Check if the player just landed
            else if (!wasGrounded && isGrounded)
            {
                // Send OSC signal indicating landing
                SendJumpSignal(false);
            }

            // Update wasGrounded for the next frame
            wasGrounded = isGrounded;
        }

        // Method to send the jump signal via OSC
        void SendJumpSignal(bool jump)
        {
            if (oscManager != null)
            {
                // Send the jump signal using the OSC manager
                // This method should be defined in your OSC_manager class
                oscManager.SendJumpSignal(jump);
            }
        }
    }
}
