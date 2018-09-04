
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public float smoothSpeed = 0.25f;
    public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 newPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, newPos, smoothSpeed);
        transform.position = smoothedPos;

        transform.LookAt(target);
    }
}
