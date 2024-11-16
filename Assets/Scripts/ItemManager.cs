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
    public float itemCleanupDistance = 20f; // Distance behind player to cleanup

    private Transform player;
    private Transform coinsParent;
    private Transform obstaclesParent;

    // Three lane positions: Left, Middle, Right
    private readonly float[] lanePositions = new float[] { -4f, 0f, 4f };

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (!player)
            Debug.LogError("Player not found! Ensure it has the 'Player' tag.");

        // Find or create global parents
        coinsParent = GameObject.Find("CoinsParent")?.transform;
        if (coinsParent == null)
        {
            coinsParent = new GameObject("CoinsParent").transform;
            coinsParent.parent = transform;
            coinsParent.position = Vector3.zero;
        }

        obstaclesParent = GameObject.Find("ObstaclesParent")?.transform;
        if (obstaclesParent == null)
        {
            obstaclesParent = new GameObject("ObstaclesParent").transform;
            obstaclesParent.parent = transform;
            obstaclesParent.position = Vector3.zero;
        }
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
                Instantiate(coinPrefab, spawnPosition, Quaternion.identity, coinsParent);
            }
        }
    }

    private void SpawnObstacles(GameObject terrain)
    {
        if (obstaclePrefabs.Length == 0)
            return;

        for (int i = 0; i < maxObstacles; i++)
        {
            if (Random.value <= obstacleSpawnChance)
            {
                Vector3 spawnPosition = GetLaneSpawnPosition(terrain);
                Instantiate(
                    obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)],
                    spawnPosition,
                    Quaternion.identity,
                    obstaclesParent
                );
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

    void Update()
    {
        CleanupItems();
    }

    private void CleanupItems()
    {
        foreach (Transform item in coinsParent)
        {
            if (item.position.z < player.position.z - itemCleanupDistance)
                Destroy(item.gameObject);
        }

        foreach (Transform item in obstaclesParent)
        {
            if (item.position.z < player.position.z - itemCleanupDistance)
                Destroy(item.gameObject);
        }
    }
}
