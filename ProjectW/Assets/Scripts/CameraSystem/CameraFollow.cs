using UnityEngine;

/// <summary>
/// This script allows the camera to smoothly follow a target (e.g., the player) with a specified offset.
/// The movement is smoothed using linear interpolation to create a more natural tracking effect.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // The target the camera follows (e.g., the player)
    [SerializeField] private float smoothSpeed = 0.125f;  // Controls how smoothly the camera follows the target
    [SerializeField] private Vector3 offset;  // The positional offset of the camera relative to the target

    /// <summary>
    /// Called every fixed frame-rate frame. 
    /// Used for smooth camera movement to avoid jitter when working with physics updates.
    /// </summary>
    void FixedUpdate()
    {
        // Calculate the desired position by adding the offset to the target's position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}

