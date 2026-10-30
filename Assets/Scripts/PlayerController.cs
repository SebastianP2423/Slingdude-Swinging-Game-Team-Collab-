using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // player movement as a whole is handled in this script,
    // while the swinging will takeover from another script
    [SerializeField] private float moveSpeed = 8f;

    // jumping variables
    [SerializeField] private float jumpForce = 12f;

    // ground check for jumping
    [SerializeField] private Transform groundCheck;

    [SerializeField] private float groundCheckRadius = 0.2f;

    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;

    private float moveInput;
    private bool isGrounded;

    // is later disabled by the separate swinging script
    public bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // automatically looks for a child object called GroundCheck
        // if one exists in the hierarchy
        if (groundCheck == null)
        {
            Transform foundGroundCheck = transform.Find("GroundCheck");

            if (foundGroundCheck != null)
            {
                groundCheck = foundGroundCheck;
            }
        }
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
        // prevents null reference errors if GroundCheck
        // has not been assigned yet
        if (groundCheck == null)
            return;

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    // ground check gizmo for debug purposes
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