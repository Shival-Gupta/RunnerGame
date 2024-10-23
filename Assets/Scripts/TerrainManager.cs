using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GameObject[] terrainPrefabs;
    public Transform playerTransform;
    public Vector3 initialSpawnPosition; // Initial spawn position parameter
    private float zSpawn; // Used for terrain spacing along the z-axis
    public float terrainLength = 30f;
    public int numberOfTerrains = 5;
    private List<GameObject> activeTerrains = new List<GameObject>();

    void Start()
    {
        zSpawn = initialSpawnPosition.z; // Set initial spawn position on z-axis

        for (int i = 0; i < numberOfTerrains; i++)
        {
            if (i == 0)
                SpawnTerrain(0); // Spawn first terrain using the initial position
            else
                SpawnTerrain(Random.Range(0, terrainPrefabs.Length));
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 35 > zSpawn - (numberOfTerrains * terrainLength))
        {
            SpawnTerrain(Random.Range(0, terrainPrefabs.Length));
            DeleteTerrain();
        }
    }

    void SpawnTerrain(int terrainIndex)
    {
        // Spawn at the current zSpawn position along the z-axis
        GameObject terrain = Instantiate(terrainPrefabs[terrainIndex], 
                                         new Vector3(initialSpawnPosition.x, initialSpawnPosition.y, zSpawn), 
                                         Quaternion.identity);
        activeTerrains.Add(terrain);
        zSpawn += terrainLength; // Move zSpawn forward for the next terrain
    }

    void DeleteTerrain()
    {
        Destroy(activeTerrains[0]);
        activeTerrains.RemoveAt(0);
    }
}
