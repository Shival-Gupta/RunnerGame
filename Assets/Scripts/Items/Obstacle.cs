using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Transform player;
    public float despawnDistance = 10f;   // Distance behind the player to despawn

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;  // Assuming player has the tag "Player"
    }

    void Update()
    {
        // Check if the obstacle is behind the player and despawn
        if (player != null && player.position.z - transform.position.z > despawnDistance)
        {
            Destroy(gameObject);
            Debug.Log("Obstacle despawned.");
        }
    }
}
