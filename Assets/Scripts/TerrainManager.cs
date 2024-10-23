using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public Transform playerTransform;         // Reference to player's transform
    public GameObject[] terrainPrefabs;       // Array of terrain prefabs
    public Vector3 initialSpawnPosition;      // Initial spawn position for terrains
    public float terrainLength = 20f;         // Length of each terrain segment
    public float spawnTriggerDistance = 400f;  // Distance from player to spawn next terrain
    public int maxInstances = 25;             // Maximum number of terrain segments

    private Vector3 nextSpawnPosition;        // Tracks the next terrain spawn position
    private List<GameObject> activeTerrains = new List<GameObject>();

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
        // Spawn terrain at the next spawn position and set it as a child of the TerrainManager GameObject
        GameObject terrain = Instantiate(terrainPrefabs[terrainIndex], nextSpawnPosition, Quaternion.identity, transform);
        activeTerrains.Add(terrain);  // Add terrain to the active terrains list

        nextSpawnPosition.z += terrainLength;  // Move the spawn position forward for the next terrain
    }

    void DeleteOldestTerrain()
    {
        // Destroy the oldest terrain segment (first in the list)
        Destroy(activeTerrains[0]);
        activeTerrains.RemoveAt(0);
    }
}
