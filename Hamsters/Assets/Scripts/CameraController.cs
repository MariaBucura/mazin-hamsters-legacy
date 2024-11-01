using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 3.0f;

    private float rotationY;
    private float rotationX;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distanceFromTarget = 8.0f;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField]
    private float smoothTime = 0.2f;

    [SerializeField]
    private Vector2 rotationXMinMax = new Vector2(-40, 40);

    [SerializeField]
    private LayerMask collisionMask;

    [SerializeField]
    private float minDistanceFromTarget = 1.0f;

    [SerializeField]
    private float collissionBuffer = 0.3f;

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX += mouseY;
        rotationY += mouseX;

        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(rotationX, rotationY);

        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
        transform.localEulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTarget;

        RaycastHit hit;

        Vector3 directionToCamera = -transform.forward;
        float desiredDistance = distanceFromTarget;

        if(Physics.Raycast(target.position, directionToCamera, out hit, distanceFromTarget, collisionMask))
        {
            desiredDistance = Mathf.Clamp(hit.distance - collissionBuffer, minDistanceFromTarget, distanceFromTarget);
        }

        transform.position = target.position - transform.forward * desiredDistance;

    }
}
