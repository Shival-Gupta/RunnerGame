using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 2f;
    public float spawnDistance = 30f;
    public Transform playerTransform;

    void Start()
    {
        if (playerTransform != null) // Null check to avoid issues
        {
            InvokeRepeating("SpawnObstacle", 1f, spawnRate);
        }
    }

    void SpawnObstacle()
    {
        if (playerTransform != null) // Double-check before using
        {
            Vector3 spawnPos = new Vector3(Random.Range(-2f, 2f), 0.5f, playerTransform.position.z + spawnDistance);
            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        }
    }
}
