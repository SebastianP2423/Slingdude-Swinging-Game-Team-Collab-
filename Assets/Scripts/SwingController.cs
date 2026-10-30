using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DistanceJoint2D))]
public class SwingController : MonoBehaviour
{
    // swing point is the point in the world where the player will swing from
    [SerializeField] private Transform swingPoint;

    // controls how much force can be applied while swinging
    [SerializeField] private float swingForce = 10f;

    private Rigidbody2D rb;
    private DistanceJoint2D joint;
    private PlayerController playerController;

    private bool isSwinging = false;

    // the player controller is used to disable movement while swinging
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
        playerController = GetComponent<PlayerController>();

        joint.enabled = false;
    }

    private void Update()
    {
        // press e to start swinging
        if (Input.GetKeyDown(KeyCode.E) && !isSwinging)
        {
            StartSwing();
        }

        // release e to stop swinging
        if (Input.GetKeyUp(KeyCode.E) && isSwinging)
        {
            StopSwing();
        }
    }

    private void FixedUpdate()
    {
        if (!isSwinging)
            return;

        float moveInput = Input.GetAxisRaw("Horizontal");

        rb.AddForce(
            new Vector2(moveInput * swingForce, 0f),
            ForceMode2D.Force
        );
    }

    // start swinging by enabling the joint and
    // setting the connected anchor to the swing point
    private void StartSwing()
    {
        if (swingPoint == null)
            return;

        isSwinging = true;

        if (playerController != null)
        {
            playerController.canMove = false;
        }

        joint.connectedAnchor = swingPoint.position;

        joint.distance = Vector2.Distance(
            transform.position,
            swingPoint.position
        );

        joint.enabled = true;
    }

    // stop swinging by disabling the joint
    // and allowing the player to move again
    private void StopSwing()
    {
        isSwinging = false;

        if (playerController != null)
        {
            playerController.canMove = true;
        }

        joint.enabled = false;
    }

    // draws a line to the swing point for debugging
    private void OnDrawGizmosSelected()
    {
        if (swingPoint == null)
            return;

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(
            transform.position,
            swingPoint.position
        );
    }
}
