using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Movement Settings")]
    [SerializeField] [Range(1f, 50f)] public float baseSpeed = 5f;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float laneDistance = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float fallMultiplier = 2.5f; // Variable for fall speed

    [Header("Lane Management")]
    private int currentLane = 1; // 0 = Left, 1 = Middle, 2 = Right

    private Rigidbody rb;
    private Vector3 direction;

    private PlayerInputActions inputActions;
    private int moveInput = 0; // -1 for left, 1 for right
    private bool isGrounded = false; // Track if the player is currently grounded

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

        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMoveCanceled;
        inputActions.Player.Jump.performed += OnJump;
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("PlayerMovement initialized. Current Lane: " + currentLane);
    }

    void Update()
    {
        // Forward movement
        direction.z = baseSpeed * speedMultiplier;

        // Lane movement
        HandleLaneMovement();

        // Smooth lane transition
        Vector3 targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);

        // Modify the downward velocity to implement fall multiplier
        if (rb.velocity.y < 0) // While falling
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // Move the player using Rigidbody
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, direction.z);
    }

    private void HandleLaneMovement()
    {
        // Lane movement logic
        if (moveInput != 0)
        {
            int targetLane = currentLane + moveInput;
            targetLane = Mathf.Clamp(targetLane, 0, 2); // Ensure the target lane is within bounds

            if (targetLane != currentLane)
            {
                Debug.Log("Moving from lane " + currentLane + " to lane " + targetLane);
                currentLane = targetLane;
                moveInput = 0; // Reset move input after changing lanes
            }
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded) // Check if jump input is received and player is grounded
        {
            Jump();
        }
    }

    private void Jump()
    {
        Debug.Log("Jumping with Impulse force: " + jumpForce);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; // Set grounded to false when jumping
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if player has landed on the terrain
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isGrounded = true; // Set grounded to true when landing
        }
    }

    public void IncreaseDifficulty()
    {
        speedMultiplier += 0.1f; // Increase player speed
        Debug.Log("Increased difficulty. New speed multiplier: " + speedMultiplier);
    }

    // Input System Callbacks
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (input.x < 0)
        {
            moveInput = -1; // Move left
            Debug.Log("Input received: Move Left");
        }
        else if (input.x > 0)
        {
            moveInput = 1; // Move right
            Debug.Log("Input received: Move Right");
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = 0;
        Debug.Log("Move input canceled");
    }
}
