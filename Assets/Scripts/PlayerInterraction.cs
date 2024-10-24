using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with: " + other.gameObject.name); // Log the object name

        if (other.CompareTag("Coin"))
        {
            CollectCoin(other.gameObject); // Collect the coin
        }
        else if (other.CompareTag("Obstacle"))
        {
            HitObstacle(other); // Pass the collider of the obstacle to HitObstacle
        }
    }

    private void CollectCoin(GameObject coin)
    {
        Destroy(coin); // Destroy the coin object
        // Update UI for collected coin
        UIController.instance.CollectCoin();
        // Update score
        GameController.instance.AddScore(GameController.instance.coinValue);
        Debug.Log("Coin collected!");
    }

    private void HitObstacle(Collider obstacle)
    {
        GameController.instance.ReduceLife(); // Reduce player lives
        Debug.Log("Hit an obstacle! Lives remaining: " + GameController.instance.playerLives);

        // Disable the collider to prevent further collisions
        obstacle.enabled = false; // Disable collider

        // Destroy the obstacle GameObject
        Destroy(obstacle.gameObject); // This will destroy the obstacle GameObject
    }
}
