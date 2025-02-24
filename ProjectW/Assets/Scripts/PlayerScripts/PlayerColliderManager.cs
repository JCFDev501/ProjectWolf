using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player's colliders, ensuring correct flipping and position adjustments
/// when the player changes direction. Handles hitbox and ground check offsets.
/// </summary>
public class PlayerColliderManager : MonoBehaviour
{
    private BoxCollider2D[] m_boxColliders; // Array to hold all BoxCollider2D components attached to the player
    private PlayerMovement m_playerMovement; // Reference to the PlayerMovement script for tracking player direction
    private GameObject m_playerHitBox; // Reference to the player's hitbox for attack collisions

    private Vector2 m_offset1; // Stores the initial offset of the first collider
    private Vector2 m_groundCheckOffset; // Stores the initial ground check offset

    private Enums.PlayerDirection m_lastDirection; // Keeps track of the player's last movement direction

    /// <summary>
    /// Initializes references and stores initial offsets for colliders and ground check.
    /// </summary>
    void Start()
    {
        // Get all BoxCollider2D components attached to the player
        m_boxColliders = GetComponents<BoxCollider2D>();

        // Cache the player hitbox
        m_playerHitBox = gameObject.transform.GetChild(0).gameObject;

        if (m_boxColliders.Length >= 1)
        {
            // Store the offset of the first BoxCollider2D
            m_offset1 = m_boxColliders[0].offset;
        }

        // Get the PlayerMovement component
        m_playerMovement = GetComponent<PlayerMovement>();

        if (m_playerMovement == null)
        {
            Debug.LogError("Cannot find PlayerMovement component.");
        }
        else
        {
            // Store the initial groundCheckOffset
            m_groundCheckOffset = m_playerMovement.groundCheckOffset;
        }

        // Initialize the last direction with the player's current direction
        m_lastDirection = m_playerMovement.GetPlayerDirection();
    }

    /// <summary>
    /// Checks for changes in player direction and updates offsets accordingly.
    /// </summary>
    void Update()
    {
        Enums.PlayerDirection currentDirection = m_playerMovement.GetPlayerDirection();

        // Check if the player's direction has changed
        if (m_lastDirection != currentDirection)
        {
            if (currentDirection == Enums.PlayerDirection.Right && m_lastDirection == Enums.PlayerDirection.Left)
            {
                // Transitioning from left to right; adjust offsets
                FlipOffsets();
            }
            else if (currentDirection == Enums.PlayerDirection.Left && m_lastDirection == Enums.PlayerDirection.Right)
            {
                // Transitioning from right to left; adjust offsets
                FlipOffsets();
            }
            else if (currentDirection == Enums.PlayerDirection.Right && m_lastDirection == Enums.PlayerDirection.None)
            {
                // Player starts moving right after being idle
                AdjustOffsetsForInitialDirection(currentDirection);
            }
            else if (currentDirection == Enums.PlayerDirection.Left && m_lastDirection == Enums.PlayerDirection.None)
            {
                // Player starts moving left after being idle
                AdjustOffsetsForInitialDirection(currentDirection);
            }

            // Update the last direction to the current one
            m_lastDirection = currentDirection;
        }
    }

    /// <summary>
    /// Flips the offsets when the player changes direction.
    /// </summary>
    void FlipOffsets()
    {
        // Get the current local position of the hitbox
        Vector3 hitBoxLocalPosition = m_playerHitBox.transform.localPosition;

        // Flip the x position of the hitbox
        hitBoxLocalPosition.x *= -1;
        m_playerHitBox.transform.localPosition = hitBoxLocalPosition;

        // Multiply offset.x by -1 for both the collider and ground check offset
        m_offset1.x *= -1;
        m_groundCheckOffset.x *= -1;

        // Apply the updated offset to the colliders
        m_boxColliders[0].offset = m_offset1;

        // Apply the updated ground check offset
        m_playerMovement.groundCheckOffset = m_groundCheckOffset;
    }

    /// <summary>
    /// Adjusts offsets for when the player starts moving after being idle.
    /// Ensures the correct offset is applied based on the player's facing direction.
    /// </summary>
    /// <param name="direction">The direction the player is moving towards.</param>
    void AdjustOffsetsForInitialDirection(Enums.PlayerDirection direction)
    {
        if (direction == Enums.PlayerDirection.Right)
        {
            // Ensure offsets are set correctly when the player starts moving right
            if (m_offset1.x < 0) m_offset1.x *= -1;
            if (m_groundCheckOffset.x < 0) m_groundCheckOffset.x *= -1;

            // Ensure the hitbox position is set correctly for right-facing direction
            Vector3 hitBoxLocalPosition = m_playerHitBox.transform.localPosition;
            if (hitBoxLocalPosition.x < 0) hitBoxLocalPosition.x *= -1;
            m_playerHitBox.transform.localPosition = hitBoxLocalPosition;
        }
        else if (direction == Enums.PlayerDirection.Left)
        {
            // Ensure offsets are set correctly when the player starts moving left
            if (m_offset1.x > 0) m_offset1.x *= -1;
            if (m_groundCheckOffset.x > 0) m_groundCheckOffset.x *= -1;

            // Ensure the hitbox position is set correctly for left-facing direction
            Vector3 hitBoxLocalPosition = m_playerHitBox.transform.localPosition;
            if (hitBoxLocalPosition.x > 0) hitBoxLocalPosition.x *= -1;
            m_playerHitBox.transform.localPosition = hitBoxLocalPosition;
        }

        // Apply the adjusted offsets to the colliders and ground check
        m_boxColliders[0].offset = m_offset1;
        m_playerMovement.groundCheckOffset = m_groundCheckOffset;
    }
}
