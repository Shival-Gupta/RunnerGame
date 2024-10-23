using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 2f;
    public float spawnDistance = 30f;
    public Transform playerTransform;
    public Vector3 initialSpawnPosition; // Initial spawn position for obstacles

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 1f, spawnRate); // Start spawning obstacles
    }

    void SpawnObstacle()
    {
        // Spawn at random x but initialSpawnPosition for y and z
        Vector3 spawnPos = new Vector3(Random.Range(-2f, 2f), initialSpawnPosition.y, playerTransform.position.z + spawnDistance);
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }
}
