using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Movement Settings")]
    [SerializeField] public float baseSpeed = 5f;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float laneDistance = 2f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Lane Management")]
    private int currentLane = 1; // 0 = Left, 1 = Middle, 2 = Right

    private Rigidbody rb;
    private Vector3 direction;

    private PlayerInputActions inputActions;
    private bool jumpInput = false;
    private int moveInput = 0; // -1 for left, 1 for right

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

        // Jumping logic
        if (jumpInput && rb.velocity.y == 0) // Check if grounded
        {
            PerformJump();
            jumpInput = false; // Reset jump input after jumping
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

    private void PerformJump()
    {
        Debug.Log("Jumping with Impulse force: " + jumpForce);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

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

    private void OnJump(InputAction.CallbackContext context)
    {
        jumpInput = true;
        Debug.Log("Jump input received");
    }
}
