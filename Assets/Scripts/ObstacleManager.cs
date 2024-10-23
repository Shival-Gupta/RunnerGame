using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public Transform playerTransform;      // Reference to player's transform
    public GameObject[] obstaclePrefabs;   // Array of obstacle prefabs
    public Vector3 initialSpawnPosition;   // Initial spawn position for obstacles
    public float spawnRate = 2f;           // Time interval between spawns
    public int maxInstances = 20;          // Maximum number of obstacles on screen

    private List<GameObject> activeObstacles = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 1f, spawnRate);  // Repeatedly spawn obstacles
    }

    void SpawnObstacle()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(-2f, 2f), 0.5f, playerTransform.position.z + initialSpawnPosition.z
        );

        int prefabIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject obstacle = Instantiate(obstaclePrefabs[prefabIndex], spawnPos, Quaternion.identity, transform);
        activeObstacles.Add(obstacle);

        // Maintain maximum obstacle limit
        if (activeObstacles.Count > maxInstances)
        {
            DestroyOldestObstacle();
        }
    }

    void DestroyOldestObstacle()
    {
        Destroy(activeObstacles[0]);
        activeObstacles.RemoveAt(0);
    }
}
