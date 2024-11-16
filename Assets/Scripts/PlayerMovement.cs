using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] [Range(1f, 50f)] public float baseSpeed = 5f;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float fallMultiplier = 2.5f;

    [Header("Lane Management")]
    [SerializeField] private float laneDistance = 4f; // Updated to 4f for consistent lanes
    private int currentLane = 1; // 0 = Left, 1 = Middle, 2 = Right

    private Rigidbody rb;
    private bool isGrounded;

    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>().x);
        inputActions.Player.Jump.performed += ctx => Jump();
        inputActions.Player.Enable();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, baseSpeed * speedMultiplier);
        Vector3 targetPosition = new Vector3((currentLane - 1) * laneDistance, rb.position.y, rb.position.z);
        rb.position = Vector3.Lerp(rb.position, targetPosition, Time.deltaTime * 10);
    }

    private void Move(float direction)
    {
        if (direction < 0 && currentLane > 0) currentLane--;
        else if (direction > 0 && currentLane < 2) currentLane++;
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain")) isGrounded = true;
    }
}
