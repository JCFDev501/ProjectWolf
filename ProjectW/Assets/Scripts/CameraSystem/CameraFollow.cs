using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // The player's transform
    [SerializeField] private float smoothSpeed = 0.125f;  // Smoothing speed
    [SerializeField] private Vector3 offset;  // Offset from the player position

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optional: Make the camera look at the player
        // transform.LookAt(target);
    }
}
