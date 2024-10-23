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
    private int currentLane = 1;  // 0 = Left, 1 = Middle, 2 = Right

    private Rigidbody rb;
    private Vector3 direction;

    private PlayerInputActions inputActions;
    private bool jumpInput = false;
    private int moveInput = 0; // -1 for left, 1 for right

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize the input actions
        inputActions = new PlayerInputActions();

        // Register input callbacks
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMoveCanceled;
        inputActions.Player.Jump.performed += OnJump;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent the Rigidbody from rotating
        Debug.Log("PlayerMovement initialized. Current Lane: " + currentLane);
    }

    private void FixedUpdate()
    {
        // Forward movement
        direction.z = baseSpeed * speedMultiplier;

        // Lane movement
        HandleLaneMovement();

        // Smooth lane transition
        Vector3 targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * 10);

        // Move the player with Rigidbody
        rb.MovePosition(smoothPosition + direction * Time.fixedDeltaTime);

        // Jumping logic
        if (jumpInput && IsGrounded())
        {
            Debug.Log("Jumping with force: " + jumpForce);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpInput = false; // Reset jump input after jumping
        }
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

    public void IncreaseDifficulty()
    {
        speedMultiplier += 0.1f;  // Increase player speed
        Debug.Log("Increased difficulty. New speed multiplier: " + speedMultiplier);
    }

    private bool IsGrounded()
    {
        // Adjust the raycast length based on your player height and collider size
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
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
