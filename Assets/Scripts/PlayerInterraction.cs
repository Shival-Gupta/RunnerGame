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
            HitObstacle(); // Hit the obstacle
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

    private void HitObstacle()
    {
        GameController.instance.ReduceLife(); // Reduce player lives
        Debug.Log("Hit an obstacle!");
    }
}
