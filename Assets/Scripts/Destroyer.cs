using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [Header("Destruction Settings")]
    public bool destroyCoins = true;      // Checkbox to destroy coins
    public bool destroyObstacles = true;  // Checkbox to destroy obstacles
    public bool destroyTerrain = false;    // Checkbox to destroy terrain

    public Transform player; // Reference to the player's transform
    public float zOffset = -10f; // Offset to follow the player

    private void Update()
    {
        // Update the position of the destroyer to follow the player
        if (player != null)
        {
            Vector3 newPosition = player.position; // Get player's position
            newPosition.z += zOffset; // Apply the offset
            transform.position = newPosition; // Update destroyer's position
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with: " + other.gameObject.name);
        // Check if the object is a coin and if destruction is enabled for coins
        if (other.CompareTag("Coin") && destroyCoins)
        {
            Destroy(other.gameObject); // Destroy the coin
            Debug.Log("Destroyed Coin!");
        }

        // Check if the object is an obstacle and if destruction is enabled for obstacles
        if (other.CompareTag("Obstacle") && destroyObstacles)
        {
            Destroy(other.gameObject); // Destroy the obstacle
            Debug.Log("Destroyed Obstacle!");
        }

        // Check if the object is terrain and if destruction is enabled for terrain
        if (other.CompareTag("Terrain") && destroyTerrain)
        {
            Destroy(other.gameObject); // Destroy the terrain (if desired)
            Debug.Log("Destroyed Terrain!");
        }
    }
}
