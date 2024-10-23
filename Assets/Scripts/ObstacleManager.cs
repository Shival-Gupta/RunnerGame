using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab;          // Obstacle prefab to be spawned
    public Transform playerTransform;          // Reference to the player's position
    public float spawnRate = 2f;               // How often to spawn obstacles
    public float spawnDistance = 30f;          // How far ahead of the player to spawn obstacles
    public Vector3 initialSpawnPosition = Vector3.zero; // Full initial spawn position for the first obstacle
    public int maxInstances = 20;              // Maximum number of obstacle instances on screen

    private List<GameObject> spawnedObstacles = new List<GameObject>(); // Tracks active obstacles

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 1f, spawnRate);
    }

    void SpawnObstacle()
    {
        // Spawn obstacle with random X within range and full initial position for the first spawn
        Vector3 spawnPos = new Vector3(Random.Range(-2f, 2f), initialSpawnPosition.y, playerTransform.position.z + spawnDistance + initialSpawnPosition.z);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity, transform);
        spawnedObstacles.Add(obstacle); // Add to list of active obstacles

        // Check if maxInstances has been exceeded
        if (spawnedObstacles.Count > maxInstances)
        {
            DeleteOldestObstacle();
        }
    }

    void DeleteOldestObstacle()
    {
        // Destroy the oldest obstacle (first in the list)
        Destroy(spawnedObstacles[0]);
        spawnedObstacles.RemoveAt(0);
    }
}
