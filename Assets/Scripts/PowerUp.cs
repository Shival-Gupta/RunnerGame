using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float boost = 10f;
    public float boostDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                StartCoroutine(ApplySpeedBoost(playerMovement));
            }
            Destroy(gameObject); // Destroy the power-up object after pickup
        }
    }

    private System.Collections.IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        playerMovement.boost += boost;
        yield return new WaitForSeconds(boostDuration);
        playerMovement.boost -= boost;
    }
}
