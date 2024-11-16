using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public Transform playerTransform; // Reference to player
    public GameObject[] terrainPrefabs; // Terrain prefabs array
    public Vector3 initialSpawnPosition; // Initial spawn position
    public float terrainLength = 50f; // Length of each terrain
    public float spawnTriggerDistance = 30f; // Distance to trigger next terrain
    public int maxInstances = 5; // Max terrain segments
    public ItemManager itemManager; // Reference to ItemManager

    private Vector3 nextSpawnPosition; // Next spawn position
    private Queue<GameObject> activeTerrains = new Queue<GameObject>(); // Queue for active terrains

    void Start()
    {
        nextSpawnPosition = initialSpawnPosition;

        // Initial terrain spawn
        for (int i = 0; i < maxInstances; i++)
        {
            SpawnTerrain(Random.Range(0, terrainPrefabs.Length));
        }
    }

    void Update()
    {
        // Spawn new terrain when player is close enough
        if (playerTransform.position.z + spawnTriggerDistance > nextSpawnPosition.z)
        {
            SpawnTerrain(Random.Range(0, terrainPrefabs.Length));
            if (activeTerrains.Count > maxInstances)
            {
                DeleteOldestTerrain();
            }
        }
    }

    void SpawnTerrain(int terrainIndex)
    {
        GameObject terrain = Instantiate(terrainPrefabs[terrainIndex], nextSpawnPosition, Quaternion.identity, transform);
        activeTerrains.Enqueue(terrain);
        nextSpawnPosition.z += terrainLength;

        // Spawn items on new terrain
        itemManager?.SpawnItems(terrain);
    }

    void DeleteOldestTerrain()
    {
        GameObject oldestTerrain = activeTerrains.Dequeue();
        Destroy(oldestTerrain);
    }
}
