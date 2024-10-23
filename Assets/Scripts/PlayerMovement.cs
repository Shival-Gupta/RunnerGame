using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float baseSpeed = 5f;
    public float speedMultiplier = 1f;
    public float laneDistance = 2f;
    private int currentLane = 1;  // Middle lane as default

    private CharacterController controller;
    private Vector3 direction;
    private float gravity = -9.81f;
    private float jumpForce = 5f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Forward movement
        direction.z = baseSpeed * speedMultiplier;

        // Lane-based movement
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
        {
            currentLane--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2)
        {
            currentLane++;
        }

        // Smooth transition between lanes
        Vector3 targetPosition = transform.position + new Vector3((currentLane - 1) * laneDistance, 0, 0);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);

        // Jumping logic
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                direction.y = jumpForce;
            }
        }

        // Apply gravity
        direction.y += gravity * Time.deltaTime;

        // Move the player
        controller.Move(direction * Time.deltaTime);
    }

    public void IncreaseDifficulty()
    {
        speedMultiplier += 0.1f;  // Increase player speed
    }
}
