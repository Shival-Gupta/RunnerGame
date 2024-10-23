using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Item Prefabs")]
    public GameObject[] powerupPrefabs;  // Array of powerup prefabs
    public GameObject[] obstaclePrefabs; // Array of obstacle prefabs
    public GameObject coinPrefab;        // Coin prefab

    [Header("Item Spawn Settings")]
    public int maxCoins = 10;              // Maximum number of coins per terrain
    public int maxPowerups = 3;            // Maximum number of powerups per terrain
    public int maxObstacles = 5;           // Maximum number of obstacles per terrain

    public float coinSpawnChance = 0.7f;   // Chance to spawn a coin
    public float powerupSpawnChance = 0.2f; // Chance to spawn a powerup
    public float obstacleSpawnChance = 0.5f; // Chance to spawn an obstacle

    private GameObject itemParent;         // Parent for all spawned items

    void Start()
    {
        // Create a parent GameObject to organize all spawned items
        itemParent = new GameObject("SpawnedItems");
        itemParent.transform.parent = transform;
    }

    public void SpawnItems(GameObject terrain)
    {
        SpawnCoins(terrain);
        SpawnPowerups(terrain);
        SpawnObstacles(terrain);
    }

    private void SpawnCoins(GameObject terrain)
    {
        for (int i = 0; i < maxCoins; i++)
        {
            if (Random.value <= coinSpawnChance)
            {
                Vector3 spawnPosition = GetRandomPositionOnTerrain(terrain);
                Instantiate(coinPrefab, spawnPosition, Quaternion.identity, itemParent.transform);
            }
        }
    }

    private void SpawnPowerups(GameObject terrain)
    {
        for (int i = 0; i < maxPowerups; i++)
        {
            if (Random.value <= powerupSpawnChance && powerupPrefabs.Length > 0)
            {
                Vector3 spawnPosition = GetRandomPositionOnTerrain(terrain);
                GameObject powerup = Instantiate(powerupPrefabs[Random.Range(0, powerupPrefabs.Length)], spawnPosition, Quaternion.identity, itemParent.transform);
            }
        }
    }

    private void SpawnObstacles(GameObject terrain)
    {
        for (int i = 0; i < maxObstacles; i++)
        {
            if (Random.value <= obstacleSpawnChance && obstaclePrefabs.Length > 0)
            {
                Vector3 spawnPosition = GetRandomPositionOnTerrain(terrain);
                GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], spawnPosition, Quaternion.identity, itemParent.transform);
            }
        }
    }

    private Vector3 GetRandomPositionOnTerrain(GameObject terrain)
    {
        // Get the bounds of the terrain to calculate random positions
        Renderer terrainRenderer = terrain.GetComponent<Renderer>();
        if (terrainRenderer == null) return Vector3.zero;

        Vector3 terrainMin = terrainRenderer.bounds.min;
        Vector3 terrainMax = terrainRenderer.bounds.max;

        float randomX = Random.Range(terrainMin.x, terrainMax.x);
        float randomZ = Random.Range(terrainMin.z, terrainMax.z);

        return new Vector3(randomX, terrainMax.y + 1f, randomZ);  // Spawn slightly above the terrain
    }

    public void RemoveItemsFromTerrain(GameObject terrain)
    {
        // Optionally, implement logic to clean up old items when terrain is removed
        foreach (Transform item in itemParent.transform)
        {
            if (item.position.y < terrain.transform.position.y)
            {
                Destroy(item.gameObject);
            }
        }
    }
}
