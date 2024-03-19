using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag; // stopping speed and ground resistance

    [Header("Jumping")]
    public float jumpForce;
    public float gravity;
    public float jumpCooldown;
    public float airMultiplier; // movement control in air
    bool readyToJump = true;
    Vector3 velocity;

    [Header("Ground Check")]
    //Ground check
    public float playerHeight;
    public LayerMask playerMask;
    public bool isGrounded;

    public Transform orientation;

    // input
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }
    private void FixedUpdate()
    {
        // ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ~playerMask);
        
        SpeedControl();

        //handle drag
        if (isGrounded)
            rigidBody.drag = groundDrag;
        else
            rigidBody.drag = 0;
    }        
    public void ProcessMove(Vector2 input)
    {
        //calculate movement direction
        moveDirection = orientation.forward * input.y + orientation.right * input.x;

        //extra gravity
        rigidBody.AddForce(Vector3.down * gravity);

        if (isGrounded) //on ground
            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!isGrounded) // in air
            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

    }
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        //limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z);
        }
    }
    public void Jump()
    {
        if (readyToJump && isGrounded)
        {
            readyToJump = false;

            // reset y 
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
            // jump
            rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }    
    private void ResetJump()
    {
        readyToJump = true;
    }

}
