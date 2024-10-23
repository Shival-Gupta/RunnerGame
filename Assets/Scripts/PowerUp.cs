using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float boostAmount = 10f;
    public float boostDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                StartCoroutine(ApplySpeedBoost(player));
            }
            Destroy(gameObject); // Destroy the power-up once collected
        }
    }

    private System.Collections.IEnumerator ApplySpeedBoost(PlayerMovement player)
    {
        player.baseSpeed += boostAmount;
        yield return new WaitForSeconds(boostDuration);
        player.baseSpeed -= boostAmount;
    }
}
