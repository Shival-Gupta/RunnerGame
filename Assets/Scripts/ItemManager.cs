using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("Item Prefabs")]
    public GameObject coinPrefab;        // Coin prefab
    public GameObject[] obstaclePrefabs; // Array of obstacle prefabs

    [Header("Item Spawn Settings")]
    public int maxCoins = 10;              // Maximum number of coins per terrain
    public int maxObstacles = 5;           // Maximum number of obstacles per terrain
    public int maxCoinInstances = 30;      // Maximum total coin instances
    public int maxObstacleInstances = 20;  // Maximum total obstacle instances
    public float coinSpawnChance = 0.7f;   // Chance to spawn a coin
    public float obstacleSpawnChance = 0.5f;// Chance to spawn an obstacle

    [Header("Lane Settings")]
    public float laneWidth = 2f;          // Width of each lane
    public float terrainLength = 20f;     // Length of each terrain

    private List<GameObject> activeCoins = new List<GameObject>();      // List of active coins
    private List<GameObject> activeObstacles = new List<GameObject>();  // List of active obstacles
    private Transform player;              // Reference to the player

    private readonly float[] lanePositions = new float[] { -2f, 0f, 2f }; // Fixed lane positions

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player object not found. Ensure the Player has the 'Player' tag.");
        }
    }

    void Update()
    {
        // No longer need to despawn items here
    }

    public void SpawnItems(GameObject terrain)
    {
        Debug.Log($"Spawning items on terrain: {terrain.name}");

        if (activeCoins.Count < maxCoinInstances)
            SpawnCoins(terrain);

        if (activeObstacles.Count < maxObstacleInstances)
            SpawnObstacles(terrain);
    }

    private void SpawnCoins(GameObject terrain)
    {
        Debug.Log("Attempting to spawn coins...");
        for (int i = 0; i < maxCoins; i++)
        {
            if (Random.value <= coinSpawnChance)
            {
                Vector3 spawnPosition = GetRandomLaneSpawnPosition(terrain);
                GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
                activeCoins.Add(coin);
                Debug.Log($"Coin spawned at: {spawnPosition}");
            }
        }
    }

    private void SpawnObstacles(GameObject terrain)
    {
        Debug.Log("Attempting to spawn obstacles...");
        for (int i = 0; i < maxObstacles; i++)
        {
            if (Random.value <= obstacleSpawnChance && obstaclePrefabs.Length > 0)
            {
                Vector3 spawnPosition = GetRandomLaneSpawnPosition(terrain);
                GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], spawnPosition, Quaternion.identity);
                activeObstacles.Add(obstacle);
                Debug.Log($"Obstacle spawned at: {spawnPosition}");
            }
        }
    }

    private Vector3 GetRandomLaneSpawnPosition(GameObject terrain)
    {
        // Randomly select a lane position
        float laneX = lanePositions[Random.Range(0, lanePositions.Length)];
        float randomZ = Random.Range(terrain.transform.position.z - terrainLength / 2f, terrain.transform.position.z + terrainLength / 2f);
        return new Vector3(laneX, terrain.transform.position.y + 1f, randomZ); // Spawn slightly above the terrain
    }
}
