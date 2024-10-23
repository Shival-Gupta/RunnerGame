using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;        // Array of obstacles
    public Transform playerTransform;           // Reference to the player
    public float spawnDistance = 50f;           // Distance ahead of the player to spawn obstacles
    public int maxInstances = 15;               // Max obstacles on screen
    public float spawnRate = 2f;                // Time between spawns
    public float laneDistance = 4f;             // Distance between lanes

    private List<GameObject> activeObstacles = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 2f, spawnRate);
    }

    void SpawnObstacle()
    {
        int randomLane = Random.Range(0, 3); // Choose a lane: 0=left, 1=center, 2=right
        int randomIndex = Random.Range(0, obstaclePrefabs.Length); // Choose random obstacle

        // Spawn position in the chosen lane, ahead of the player
        Vector3 spawnPos = playerTransform.position + new Vector3((randomLane - 1) * laneDistance, 0, spawnDistance);
        GameObject newObstacle = Instantiate(obstaclePrefabs[randomIndex], spawnPos, Quaternion.identity, transform);
        activeObstacles.Add(newObstacle);

        if (activeObstacles.Count > maxInstances)
        {
            DeleteOldestObstacle();
        }
    }

    void DeleteOldestObstacle()
    {
        Destroy(activeObstacles[0]);
        activeObstacles.RemoveAt(0);
    }
}
