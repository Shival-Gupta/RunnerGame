using UnityEngine;
using UnityEngine.InputSystem;

public class DifficultyManager : MonoBehaviour
{
    public float speedIncreaseRate = 0.1f; // How much to increase speed each time
    public float maxSpeed = 10f; // Maximum speed limit
    private float currentSpeedMultiplier = 1f;

    void Update()
    {
        // Gradually increase speed as the game progresses
        if (GameController.instance.playerScore > 500) // Example condition based on score
        {
            currentSpeedMultiplier += speedIncreaseRate * Time.deltaTime;
            currentSpeedMultiplier = Mathf.Clamp(currentSpeedMultiplier, 1f, maxSpeed);
        }

        // Apply this increased multiplier to the player movement
        PlayerMovementController playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
        if (playerMovement != null)
        {
            playerMovement.speedMultiplier = currentSpeedMultiplier;
        }
    }
}
