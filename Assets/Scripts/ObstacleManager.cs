using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;  // Array of different obstacle prefabs
    public int obstaclesPerTerrain = 3;   // Number of obstacles to spawn per terrain
    public float laneWidth = 2f;          // Width of each lane (e.g., 3 lanes: left, center, right)
    public float terrainLength = 20f;     // Length of each terrain

    private List<GameObject> activeObstacles = new List<GameObject>();  // Track active obstacles

    // Spawn obstacles on the given terrain segment
    public void SpawnObstacles(GameObject terrain)
    {
        // Get the terrain's position
        Vector3 terrainPosition = terrain.transform.position;

        // Define 3 lanes: left, center, right
        float[] lanePositions = new float[] { -laneWidth, 0, laneWidth };

        // Spawn the specified number of obstacles on the terrain
        for (int i = 0; i < obstaclesPerTerrain; i++)
        {
            // Pick a random lane
            float laneX = lanePositions[Random.Range(0, lanePositions.Length)];
            
            // Random Z position within the terrain's length
            float spawnZ = Random.Range(terrainPosition.z - terrainLength / 2, terrainPosition.z + terrainLength / 2);

            // Create the obstacle at the chosen lane and position
            Vector3 spawnPosition = new Vector3(laneX, terrainPosition.y, spawnZ);

            // Random obstacle type
            int prefabIndex = Random.Range(0, obstaclePrefabs.Length);

            // Instantiate obstacle and set parent to ObstacleManager
            GameObject obstacle = Instantiate(obstaclePrefabs[prefabIndex], spawnPosition, Quaternion.identity);
            obstacle.transform.SetParent(this.transform);  // Set ObstacleManager as parent

            activeObstacles.Add(obstacle);  // Keep track of active obstacles
        }
    }

    // Optional: Remove obstacles when a terrain is destroyed
    public void RemoveObstaclesFromTerrain(GameObject terrain)
    {
        // Iterate over the active obstacles and remove those within the destroyed terrain's Z-range
        List<GameObject> obstaclesToRemove = new List<GameObject>();
        float terrainZMin = terrain.transform.position.z - terrainLength / 2;
        float terrainZMax = terrain.transform.position.z + terrainLength / 2;

        foreach (GameObject obstacle in activeObstacles)
        {
            if (obstacle.transform.position.z >= terrainZMin && obstacle.transform.position.z <= terrainZMax)
            {
                obstaclesToRemove.Add(obstacle);
                Destroy(obstacle);
            }
        }

        // Remove the obstacles from the active list
        foreach (GameObject obstacle in obstaclesToRemove)
        {
            activeObstacles.Remove(obstacle);
        }
    }
}
