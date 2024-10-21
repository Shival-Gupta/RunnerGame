using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GameObject[] terrainPrefabs;
    public Transform playerTransform;
    private float zSpawn = 0;
    public float terrainLength = 30f;
    public int numberOfTerrains = 5;
    private List<GameObject> activeTerrains = new List<GameObject>();

    void Start()
    {
        if (playerTransform != null)
        {
            for (int i = 0; i < numberOfTerrains; i++)
            {
                if (i == 0)
                    SpawnTerrain(0); // Always spawn the first terrain at index 0
                else
                    SpawnTerrain(Random.Range(0, terrainPrefabs.Length));
            }
        }
    }

    void Update()
    {
        if (playerTransform != null && playerTransform.position.z - 35 > zSpawn - (numberOfTerrains * terrainLength))
        {
            SpawnTerrain(Random.Range(0, terrainPrefabs.Length));
            DeleteTerrain();
        }
    }

    void SpawnTerrain(int terrainIndex)
    {
        GameObject terrain = Instantiate(terrainPrefabs[terrainIndex], new Vector3(0, 0, zSpawn), Quaternion.identity);
        activeTerrains.Add(terrain);
        zSpawn += terrainLength;
    }

    void DeleteTerrain()
    {
        Destroy(activeTerrains[0]);
        activeTerrains.RemoveAt(0);
    }
}
