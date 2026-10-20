using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    // this allows the target to be set from the inspector
    private float smoothSpeed = 5f;
    private Vector3 offset = new Vector3(0f, 0f, -10f);

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}