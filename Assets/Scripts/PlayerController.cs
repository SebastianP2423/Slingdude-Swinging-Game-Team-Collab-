using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // player movement as a whole is handled in this script, while the swinging will takeover from another 
    private float moveSpeed = 8f;

    // jumping variables 
    private float jumpForce = 12f;

    // ground check for jumping
    private Transform groundCheck;
    private float groundCheckRadius = 0.2f;
    private LayerMask groundLayer;

    private Rigidbody2D rb;

    private float moveInput;
    private bool isGrounded;

    // is later disabled by the separate swinging script
    public bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        GroundCheck();

        if (!canMove)
            return;

        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }
    // fixed update is used for physics calculations
    private void FixedUpdate()
    {
        if (!canMove)
            return;

        rb.velocity = new Vector2(
            moveInput * moveSpeed,
            rb.velocity.y
        );
    }
    // jump function that sets the y velocity to the jump force
    private void Jump()
    {
        rb.velocity = new Vector2(
            rb.velocity.x,
            jumpForce
        );
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }
    // grouncd check gizmo for debug purposes
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );
    }
}
