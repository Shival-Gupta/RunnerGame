using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 10f;
    public float laneDistance = 4f; // Distance between lanes
    public float jumpHeight = 5f;
    public float gravity = -9.81f;
    
    private CharacterController controller;
    private Vector3 direction;
    private int desiredLane = 1; // 0=left, 1=center, 2=right

    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Move the player forward
        direction.z = baseSpeed;

        // Get input for lane changes
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(false); // Move left
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(true); // Move right
        }

        // Jumping logic
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpHeight;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // Apply gravity when in air
        }

        // Calculate the target position based on the desired lane
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        // Smoothly move the player to the target lane
        Vector3 moveDirection = Vector3.forward * baseSpeed + Vector3.up * verticalVelocity;
        controller.Move(moveDirection * Time.deltaTime);

        // Move horizontally (lane movement)
        Vector3 horizontalMove = Vector3.Lerp(transform.position, targetPosition, 10 * Time.deltaTime);
        controller.Move((horizontalMove - transform.position));
    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += goingRight ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2); // Ensure player stays within lanes
    }
}
