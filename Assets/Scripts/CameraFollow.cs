using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // player that the camera will follow
    [SerializeField] private Transform player;

    // higher values make the camera catch up faster
    [SerializeField] private float followSpeed = 5f;

    // keeps the camera behind the 2D scene
    private Vector3 offset = new Vector3(0f, 0f, -10f);

    private void LateUpdate()
    {
        // prevents errors if no player has been assigned
        if (player == null)
            return;

        // where the camera wants to move to
        Vector3 targetPosition = player.position + offset;

        // smooth movements not snappy towards the player
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }

    // draws a line thru the editor for debugging
    private void OnDrawGizmosSelected()
    {
        if (player == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, player.position);
    }
}