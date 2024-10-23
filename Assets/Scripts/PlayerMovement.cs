using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Movement Settings")]
    [SerializeField] public float baseSpeed = 5f;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float laneDistance = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Lane Management")]
    private int currentLane = 1;  // 0 = Left, 1 = Middle, 2 = Right

    private CharacterController controller;
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

        // Register input callbacks
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
        controller = GetComponent<CharacterController>();
        Debug.Log("PlayerMovement initialized. Current Lane: " + currentLane);
    }

    void Update()
    {
        // Forward movement
        direction.z = baseSpeed * speedMultiplier;

        // Lane movement
        HandleLaneMovement();

        // Jumping logic
        if (controller.isGrounded)
        {
            if (jumpInput)
            {
                Debug.Log("Jumping with force: " + jumpForce);
                direction.y = jumpForce;
                jumpInput = false; // Reset jump input after jumping
            }
        }

        // Apply gravity
        direction.y += gravity * Time.deltaTime;

        // Move the player
        controller.Move(direction * Time.deltaTime);
        
        // Smooth lane transition after moving
        Vector3 targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);

        Debug.Log("Moving player. Current position: " + transform.position);
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
