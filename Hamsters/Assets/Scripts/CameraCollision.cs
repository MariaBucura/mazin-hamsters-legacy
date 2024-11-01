using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform player;           // Reference to the player object
    public Vector3 offset;             // Desired offset between player and camera
    public float smoothSpeed = 0.125f; // Smoothing factor for the camera movement
    public float collisionBuffer = 0.2f; // Extra space to prevent clipping

    private Vector3 defaultOffset;

    void Start()
    {
        defaultOffset = offset; // Set the initial offset
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        RaycastHit hit;

        // Cast a ray from player to camera's desired position
        if (Physics.Raycast(player.position, (desiredPosition - player.position).normalized, out hit, offset.magnitude))
        {
            // If the ray hits something, move the camera closer
            desiredPosition = hit.point - (desiredPosition - player.position).normalized * collisionBuffer;
        }

        // Smoothly transition the camera position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Always look at the player
        transform.LookAt(player);
    }
}
