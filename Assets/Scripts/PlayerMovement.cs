using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 5f;
    public float boost = 0f;
    public float speedMultiplier = 1f;
    private CharacterController controller;
    private Vector3 direction;
    private float gravity = -9.81f;
    private float jumpHeight = 5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        SetSpeedBasedOnLevel();
    }

    void Update()
    {
        // Forward movement
        direction.z = (baseSpeed * speedMultiplier) + boost; // Ensure boost is added to base speed

        // Jumping
        if (controller.isGrounded)
        {
            direction.y = -1f; // Keep the player grounded
            if (Input.GetKeyDown(KeyCode.Space))
            {
                direction.y = jumpHeight;
            }
        }

        // Apply gravity
        direction.y += gravity * Time.deltaTime;

        // Move the player
        controller.Move(direction * Time.deltaTime);
    }

    void SetSpeedBasedOnLevel()
    {
        if (LevelManager.currentLevel == 1)
        {
            speedMultiplier = 1f;
        }
        else if (LevelManager.currentLevel == 2)
        {
            speedMultiplier = 1.2f;
        }
        else if (LevelManager.currentLevel == 3)
        {
            speedMultiplier = 1.5f;
        }
    }
}
