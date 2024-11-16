using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            GameController.instance.AddScore();
        }
        else if (other.CompareTag("Obstacle"))
        {
            GameController.instance.ReduceLife();
            Destroy(other.gameObject);
        }
    }
}
