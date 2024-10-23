using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public Transform playerTransform;         // Reference to player's transform
    public GameObject[] terrainPrefabs;       // Array of terrain prefabs
    public Vector3 initialSpawnPosition;      // Initial spawn position for terrains
    public float terrainLength = 50f;         // Length of each terrain segment
    public float spawnTriggerDistance = 30f;  // Distance from player to spawn next terrain
    public int maxInstances = 5;              // Maximum number of terrain segments
    public ObstacleManager obstacleManager;   // Reference to the ObstacleManager

    private Vector3 nextSpawnPosition;        // Tracks the next terrain spawn position
    private List<GameObject> activeTerrains = new List<GameObject>();  // List of active terrain segments

    void Start()
    {
        nextSpawnPosition = initialSpawnPosition;

        // Initial terrain spawning
        for (int i = 0; i < maxInstances; i++)
        {
            if (i == 0)
                SpawnTerrain(0); // Spawn the first terrain
            else
                SpawnTerrain(Random.Range(0, terrainPrefabs.Length)); // Spawn random terrains afterward
        }
    }

    void Update()
    {
        // Check if the player is close enough to spawn the next terrain
        if (playerTransform.position.z + spawnTriggerDistance > nextSpawnPosition.z)
        {
            SpawnTerrain(Random.Range(0, terrainPrefabs.Length)); // Spawn a new terrain randomly
            if (activeTerrains.Count > maxInstances)
            {
                DeleteOldestTerrain();
            }
        }
    }

    void SpawnTerrain(int terrainIndex)
    {
        // Spawn terrain at the next spawn position
        GameObject terrain = Instantiate(terrainPrefabs[terrainIndex], nextSpawnPosition, Quaternion.identity, transform);
        activeTerrains.Add(terrain);  // Add terrain to the active terrains list

        nextSpawnPosition.z += terrainLength;  // Move the spawn position forward for the next terrain

        // Spawn obstacles on the new terrain using the ObstacleManager
        if (obstacleManager != null)
        {
            obstacleManager.terrainLength = terrainLength;  // Set terrain length for obstacle spawning
            obstacleManager.SpawnObstacles(terrain);
        }
    }

    void DeleteOldestTerrain()
    {
        // Destroy the oldest terrain segment and remove associated obstacles
        GameObject oldestTerrain = activeTerrains[0];
        if (obstacleManager != null)
        {
            obstacleManager.RemoveObstaclesFromTerrain(oldestTerrain);  // Remove obstacles from this terrain
        }
        Destroy(oldestTerrain);
        activeTerrains.RemoveAt(0);
    }
}
