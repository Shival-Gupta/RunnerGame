using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [Header("Player & Terrain Settings")]
    public Transform playerTransform; // Reference to player
    public GameObject[] terrainPrefabs; // Array of terrain prefabs
    public Vector3 initialSpawnPosition; // Starting position for terrain spawning
    public float terrainLength = 20f; // Length of each terrain prefab
    public int maxInitialTerrains = 20; // Number of terrains to spawn initially
    public int itemSpawnStartIndex = 3; // Terrain index from which items start spawning

    [Header("Spawn Settings")]
    public float spawnTriggerDistance = 250f; // Distance ahead to trigger terrain spawn
    public bool spawnForward = true; // Direction of terrain spawning (true = forward, false = backward)

    private Vector3 nextSpawnPosition; // Position for the next terrain
    private Queue<GameObject> activeTerrains = new Queue<GameObject>(); // Queue to store active terrains
    private ItemManager itemManager; // Reference to ItemManager
    private int currentTerrainIndex = 0; // Tracks current terrain index

    void Start()
    {
        // Find the ItemManager in the scene
        itemManager = FindObjectOfType<ItemManager>();
        if (itemManager == null)
        {
            Debug.LogError("ItemManager not found in the scene!");
            return;
        }

        // Set the initial position for spawning
        nextSpawnPosition = initialSpawnPosition;

        // Spawn initial terrains
        for (int i = 0; i < maxInitialTerrains; i++)
        {
            int randomTerrainIndex = Random.Range(0, terrainPrefabs.Length);

            // Spawn terrain
            SpawnTerrain(randomTerrainIndex);
        }
    }

    void Update()
    {
        // Check if the player is near the next spawn point
        if (Vector3.Distance(playerTransform.position, nextSpawnPosition) < spawnTriggerDistance)
        {
            // Spawn a new terrain segment
            SpawnTerrain(Random.Range(0, terrainPrefabs.Length));

            // Delete the oldest terrain if we have more than the maximum allowed
            if (activeTerrains.Count > maxInitialTerrains)
            {
                DeleteOldestTerrain();
            }
        }
    }

    void SpawnTerrain(int terrainIndex)
    {
        // Instantiate a new terrain
        GameObject terrain = Instantiate(terrainPrefabs[terrainIndex], nextSpawnPosition, Quaternion.identity, transform);

        // Add the terrain to the active queue
        activeTerrains.Enqueue(terrain);

        // Update the next spawn position
        if (spawnForward)
        {
            nextSpawnPosition.z += terrainLength;
        }
        else
        {
            nextSpawnPosition.z -= terrainLength;
        }

        // Spawn items on terrain starting from itemSpawnStartIndex
        if (currentTerrainIndex >= itemSpawnStartIndex)
        {
            itemManager.SpawnItems(terrain); // Call item manager to spawn items
        }

        // Increment the current terrain index
        currentTerrainIndex++;
    }

    void DeleteOldestTerrain()
    {
        // Remove and destroy the oldest terrain
        GameObject oldestTerrain = activeTerrains.Dequeue();
        Destroy(oldestTerrain);
    }
}
