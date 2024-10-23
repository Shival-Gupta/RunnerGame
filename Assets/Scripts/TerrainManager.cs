using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GameObject[] terrainPrefabs;        // Array of terrain prefabs
    public Transform playerTransform;          // Player reference
    public float terrainLength = 30f;          // Length of each terrain
    public int maxInstances = 15;              // Maximum number of terrain instances
    public float spawnTriggerDistance = 30f;   // Distance from player to spawn the next terrain

    private Vector3 nextSpawnPosition;         // Where the next terrain will be spawned
    private List<GameObject> activeTerrains = new List<GameObject>();

    void Start()
    {
        nextSpawnPosition = playerTransform.position + new Vector3(0, 0, terrainLength); // Start a bit ahead

        // Spawn initial terrains
        for (int i = 0; i < maxInstances; i++)
        {
            if (i == 0)
                SpawnTerrain(0); // Always spawn the first one from the array
            else
                SpawnTerrain(Random.Range(0, terrainPrefabs.Length));
        }
    }

    void Update()
    {
        // Check if the player is close to the next spawn trigger point
        if (playerTransform.position.z + spawnTriggerDistance > nextSpawnPosition.z)
        {
            SpawnTerrain(Random.Range(0, terrainPrefabs.Length)); // Spawn new terrain
            DeleteOldestTerrain(); // Remove the oldest one
        }
    }

    void SpawnTerrain(int prefabIndex)
    {
        GameObject newTerrain = Instantiate(terrainPrefabs[prefabIndex], nextSpawnPosition, Quaternion.identity, transform);
        activeTerrains.Add(newTerrain);
        nextSpawnPosition.z += terrainLength; // Move to the next spawn point
    }

    void DeleteOldestTerrain()
    {
        Destroy(activeTerrains[0]);
        activeTerrains.RemoveAt(0);
    }
}
