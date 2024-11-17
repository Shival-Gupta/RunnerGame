using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject coinPrefab;
    public GameObject[] obstaclePrefabs;

    [Header("Spawn Settings")]
    public int maxCoins = 10;
    public int maxObstacles = 5;
    public float coinSpawnChance = 0.7f;
    public float obstacleSpawnChance = 0.5f;

    private Transform coinsParent;
    private Transform obstaclesParent;

    // Three lane positions: Left, Middle, Right
    private readonly float[] lanePositions = new float[] { -3f, 0f, 3f };

    void Awake()
    {
        // Initialize the parent containers for items
        coinsParent = new GameObject("CoinsParent").transform;
        coinsParent.position = Vector3.zero;
        coinsParent.parent = transform;
        obstaclesParent = new GameObject("ObstaclesParent").transform;
        obstaclesParent.position = Vector3.zero;
        obstaclesParent.parent = transform;
    }

    public void SpawnItems(GameObject terrain)
    {
        SpawnCoins(terrain);
        SpawnObstacles(terrain);
    }

    private void SpawnCoins(GameObject terrain)
    {
        for (int i = 0; i < maxCoins; i++)
        {
            if (Random.value <= coinSpawnChance)
            {
                Vector3 spawnPosition = GetLaneSpawnPosition(terrain);
                Instantiate(coinPrefab, spawnPosition, Quaternion.identity, terrain.transform); // Parent to terrain
            }
        }
    }

    private void SpawnObstacles(GameObject terrain)
    {
        for (int i = 0; i < maxObstacles; i++)
        {
            if (Random.value <= obstacleSpawnChance)
            {
                Vector3 spawnPosition = GetLaneSpawnPosition(terrain);
                Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], spawnPosition, Quaternion.identity, terrain.transform); // Parent to terrain
            }
        }
    }

    // Method to get a spawn position on one of the three lanes
    private Vector3 GetLaneSpawnPosition(GameObject terrain)
    {
        // Choose a random lane (-4f, 0f, or 4f)
        float laneX = lanePositions[Random.Range(0, lanePositions.Length)];
        float randomZ = Random.Range(
            terrain.transform.position.z - 10f,
            terrain.transform.position.z + 10f
        );

        // Spawn the item slightly above the terrain for visibility
        return new Vector3(laneX, terrain.transform.position.y + 1f, randomZ);
    }
}
