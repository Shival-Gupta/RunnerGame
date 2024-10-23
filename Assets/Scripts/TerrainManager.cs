using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GameObject[] terrainPrefabs;        // Terrain prefabs to be spawned
    public Transform playerTransform;          // Reference to the player's position
    public float terrainLength = 30f;          // Length of each terrain segment
    public int numberOfTerrains = 5;           // Number of terrains to initially spawn
    public int maxInstances = 20;              // Maximum number of terrain instances on screen
    public Vector3 initialSpawnPosition = Vector3.zero;  // Full initial spawn position for first terrain

    private Vector3 nextSpawnPosition;         // Tracks where the next terrain should spawn
    private List<GameObject> activeTerrains = new List<GameObject>(); // Tracks active terrain objects

    void Start()
    {
        nextSpawnPosition = initialSpawnPosition; // Start spawning from the full initial spawn position

        for (int i = 0; i < numberOfTerrains; i++)
        {
            if (i == 0)
                SpawnTerrain(0); // Always spawn the first terrain at index 0
            else
                SpawnTerrain(Random.Range(0, terrainPrefabs.Length)); // Spawn random terrains after the first
        }
    }

    void Update()
    {
        // Spawn terrain if player has moved past the trigger point
        if (playerTransform.position.z - 35 > nextSpawnPosition.z - (numberOfTerrains * terrainLength))
        {
            SpawnTerrain(Random.Range(0, terrainPrefabs.Length));
            if (activeTerrains.Count > maxInstances)
            {
                DeleteOldestTerrain();
            }
        }
    }

    void SpawnTerrain(int terrainIndex)
    {
        // Spawn terrain at the nextSpawnPosition and set as child of TerrainManager GameObject
        GameObject terrain = Instantiate(terrainPrefabs[terrainIndex], nextSpawnPosition, Quaternion.identity, transform);
        activeTerrains.Add(terrain); // Add terrain to the list of active terrains
        nextSpawnPosition.z += terrainLength;  // Update nextSpawnPosition for next terrain (increment along the Z-axis)
    }

    void DeleteOldestTerrain()
    {
        // Destroy the oldest terrain (first in the list)
        Destroy(activeTerrains[0]);
        activeTerrains.RemoveAt(0);
    }
}
