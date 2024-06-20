using UnityEngine;

public class BOIDS_Position : MonoBehaviour
{
    public Transform[] boids; // Array to hold references to all boid objects
    public Transform BOIDS_MID; // Transform to represent the middle point of all boids

    void Update()
    {
        // Check if the boids array is empty and log a warning if so
        if (boids.Length == 0)
        {
            Debug.LogWarning("No boids assigned to the BoidsParent.");
            return;
        }

        Vector3 sumPositions = Vector3.zero; // Vector to sum the positions of all boids

        // Iterate through each boid and sum their positions
        foreach (Transform boid in boids)
        {
            sumPositions += boid.position;
        }

        // Calculate the average position of all boids
        Vector3 averagePosition = sumPositions / boids.Length;

        // Debug the average position if needed
        // Debug.Log(averagePosition);

        // Set the BOIDS_MID position to the average position of all boids
        BOIDS_MID.position = averagePosition;
    }
}
