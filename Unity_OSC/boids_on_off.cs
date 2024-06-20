using UnityEngine;
using extOSC;

namespace OSCScripts
{
    public class boids_on_off : MonoBehaviour
    {
        // Public fields for configuration
        public int boidNumber = 0;               // Unique number for the boid
        public OSCReceiver receiver;             // OSC receiver for incoming messages
        public Renderer sphereRenderer;          // Renderer for the boid's sphere
        public Light boidLight;                  // Light component associated with the boid
        public float fadeInDuration = 0.2f;      // Duration of the fade in
        public float fadeOutDuration = 2.0f;     // Duration of the fade out
        public float alphaThreshold = 0.01f;     // Threshold below which the renderer is disabled

        // Private fields for internal state management
        private Material sphereMaterial;         // Material of the sphere renderer
        private bool isVisible = false;          // Visibility state of the boid
        private float fadeTimer = 0f;            // Timer for managing fade in/out
        private float currentFadeDuration;       // Current duration for fade in/out

        void Start()
        {
            // Validate if required components are assigned
            if (receiver == null)
            {
                Debug.LogError("OSCReceiver is not assigned. Please attach the OSCReceiver component manually.");
                return;
            }

            if (sphereRenderer == null)
            {
                Debug.LogError("Sphere Renderer is not assigned. Please attach the Sphere Renderer component manually.");
                return;
            }

            if (boidLight == null)
            {
                Debug.LogError("Light component is not assigned. Please attach the Light component manually.");
                return;
            }

            // Get the material of the sphere renderer
            sphereMaterial = sphereRenderer.material;

            // Set the material to transparent mode
            sphereMaterial.SetFloat("_Mode", 3);
            sphereMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            sphereMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            sphereMaterial.SetInt("_ZWrite", 0);
            sphereMaterial.DisableKeyword("_ALPHATEST_ON");
            sphereMaterial.EnableKeyword("_ALPHABLEND_ON");
            sphereMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            sphereMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            // Bind the OSC message to the handler
            receiver.Bind($"point/{boidNumber}/active", OnReceiveActiveMessage);

            // Ensure the boids are not visible at the start
            SetVisibility(false, true);
        }

        void Update()
        {
            // Handle fading in and out based on visibility state and fade timer
            if (isVisible && fadeTimer < currentFadeDuration)
            {
                fadeTimer += Time.deltaTime;
                float t = Mathf.Clamp01(fadeTimer / currentFadeDuration);
                SetAlpha(EaseIn(t));
                boidLight.intensity = EaseIn(t);
            }
            else if (!isVisible && fadeTimer > 0f)
            {
                fadeTimer -= Time.deltaTime;
                float t = Mathf.Clamp01(fadeTimer / currentFadeDuration);
                SetAlpha(EaseOut(t));
                boidLight.intensity = EaseOut(t);
            }
        }

        // Handle received OSC message
        void OnReceiveActiveMessage(OSCMessage message)
        {
            if (message == null || message.Values.Count == 0)
            {
                Debug.LogWarning("Received an invalid OSC message.");
                return;
            }

            int activeState = message.Values[0].IntValue;

            if (activeState == 1)
            {
                SetVisibility(true, false);
            }
            else if (activeState == 0)
            {
                SetVisibility(false, false);
            }
        }

        // Set the visibility of the boid with optional immediate effect
        void SetVisibility(bool visible, bool immediate)
        {
            isVisible = visible;
            currentFadeDuration = visible ? fadeInDuration : fadeOutDuration;

            if (immediate)
            {
                fadeTimer = visible ? currentFadeDuration : 0f;
                float t = visible ? 1f : 0f;
                SetAlpha(t);
                boidLight.intensity = t;
            }
            else
            {
                fadeTimer = visible ? 0f : currentFadeDuration;
            }

            // Enable the renderer if visible, otherwise disable it if alpha is below the threshold
            sphereRenderer.enabled = visible || sphereMaterial.color.a > alphaThreshold;
        }

        // Set the alpha value of the material
        void SetAlpha(float alpha)
        {
            if (sphereMaterial != null)
            {
                Color color = sphereMaterial.color;
                color.a = alpha;
                sphereMaterial.color = color;

                // Disable renderer if alpha is below the threshold
                if (alpha < alphaThreshold)
                {
                    sphereRenderer.enabled = false;
                }
                else
                {
                    sphereRenderer.enabled = true;
                }
            }
        }

        // Easing functions for smooth transitions
        float EaseIn(float t)
        {
            return t * t;
        }

        float EaseOut(float t)
        {
            return 1 - Mathf.Pow(1 - t, 2);
        }
    }
}
