using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerMovementState playerMovementState;
    private Rigidbody2D rigidBody2D;
    public SpriteRenderer spriteRenderer;
    public float jumpForce = 6f;
    public float doubleJumpForce = 6f;
    public Vector2 wallJumpForce = new Vector2(4f, 8f);
    public float wallJumpMovementCooldown = 0.2f;
    private PlayerMovement playerMovement;
    private float playerHalfHeight;
    private float playerHalfWidth;
    private bool canDoubleJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerHalfHeight = spriteRenderer.bounds.extents.y;
        playerHalfWidth = spriteRenderer.bounds.extents.x;
        playerMovement = GetComponent<PlayerMovement>();
        playerMovementState = GetComponent<PlayerMovementState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            CheckJumpType();
        }
          
    }

    private void CheckJumpType()
    {
        bool isGrounded = GetIsGrounded();

        if (isGrounded)
        {
            playerMovementState.SetMoveState(PlayerMovementState.MoveState.Jump);
            Jump(jumpForce);

        }
        else 
        {
            int direction = GetWallJumpDirection();
            if (direction == 0 && canDoubleJump && rigidBody2D.linearVelocity.y <= 0.1f)
            {
                DoubleJump();
            }
            else if (direction != 0)
            { 
                WallJump(direction);
            }
 
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GetIsGrounded();
    }

    private int GetWallJumpDirection()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right, playerHalfWidth + 0.1f, LayerMask.GetMask("Ground"))){
            return -1;
        }

        if (Physics2D.Raycast(transform.position, Vector2.left, playerHalfWidth + 0.1f, LayerMask.GetMask("Ground"))){
            return 1;
        }

        return 0;
    }

    private bool GetIsGrounded()
    {
       bool hit = Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, LayerMask.GetMask("Ground"));
        if (hit) 
        {
            canDoubleJump = true;
        }

        return hit;
    }

    private void Jump(float force)
    {
        rigidBody2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void DoubleJump()
    {
        rigidBody2D.linearVelocity = Vector2.zero;
        rigidBody2D.angularVelocity = 0;
        Jump(doubleJumpForce);
        canDoubleJump = false;
    }

    private void WallJump(int direction)
    {
        Vector2 force = wallJumpForce;
        force.x *= direction;
        rigidBody2D.linearVelocity = Vector2.zero;
        rigidBody2D.angularVelocity = 0;
        playerMovement.wallJumpCooldown = wallJumpMovementCooldown;
        rigidBody2D.AddForce(force, ForceMode2D.Impulse);
    }
}
