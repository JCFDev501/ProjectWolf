using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
    private BoxCollider2D[] m_boxColliders;
    private PlayerMovement m_playerMovement;
    private GameObject m_playerHitBox;

    private Vector2 m_offset1;
    private Vector2 m_groundCheckOffset;  // To store the initial groundCheckOffset

    private Enums.PlayerDirection m_lastDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Get all BoxCollider2D components attached to the player
        m_boxColliders = GetComponents<BoxCollider2D>();

        // Cache the player hitbox
        m_playerHitBox = gameObject.transform.GetChild(0).gameObject;

        if (m_boxColliders.Length >= 1)
        {
            // Access the first BoxCollider2D offset
            m_offset1 = m_boxColliders[0].offset;
        }

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

        // Initialize the last direction with the current direction
        m_lastDirection = m_playerMovement.GetPlayerDirection();
    }

    // Update is called once per frame
    void Update()
    {
        Enums.PlayerDirection currentDirection = m_playerMovement.GetPlayerDirection();

        // Check if the direction has changed
        if (m_lastDirection != currentDirection)
        {
            if (currentDirection == Enums.PlayerDirection.Right && m_lastDirection == Enums.PlayerDirection.Left)
            {
                // Moving from left to right: multiply offset.x by -1
                FlipOffsets();
            }
            else if (currentDirection == Enums.PlayerDirection.Left && m_lastDirection == Enums.PlayerDirection.Right)
            {
                // Moving from right to left: multiply offset.x by -1
                FlipOffsets();
            }
            else if (currentDirection == Enums.PlayerDirection.Right && m_lastDirection == Enums.PlayerDirection.None)
            {
                // Handle the case where the player starts moving to the right after being idle
                AdjustOffsetsForInitialDirection(currentDirection);
            }
            else if (currentDirection == Enums.PlayerDirection.Left && m_lastDirection == Enums.PlayerDirection.None)
            {
                // Handle the case where the player starts moving to the left after being idle
                AdjustOffsetsForInitialDirection(currentDirection);
            }

            // Update the last direction to the current one
            m_lastDirection = currentDirection;
        }
    }

    void FlipOffsets()
    {
        // Get the current local position of the hitbox
        Vector3 hitBoxLocalPosition = m_playerHitBox.transform.localPosition;

        // Flip the x position
        hitBoxLocalPosition.x *= -1;

        // Apply the flipped position back to the hitbox
        m_playerHitBox.transform.localPosition = hitBoxLocalPosition;

        // Multiply offset.x by -1 for both colliders and the ground check offset
        m_offset1.x *= -1;
        m_groundCheckOffset.x *= -1;

        // Apply the new offset to the colliders
        m_boxColliders[0].offset = m_offset1;

        // Apply the new groundCheckOffset
        m_playerMovement.groundCheckOffset = m_groundCheckOffset;
    }

    void AdjustOffsetsForInitialDirection(Enums.PlayerDirection direction)
    {
        if (direction == Enums.PlayerDirection.Right)
        {
            // If starting to move right, ensure offsets are correct for the right-facing direction
            if (m_offset1.x < 0) m_offset1.x *= -1;
            if (m_groundCheckOffset.x < 0) m_groundCheckOffset.x *= -1;

            // Ensure the hitbox position is correct for the right-facing direction
            Vector3 hitBoxLocalPosition = m_playerHitBox.transform.localPosition;
            if (hitBoxLocalPosition.x < 0) hitBoxLocalPosition.x *= -1;
            m_playerHitBox.transform.localPosition = hitBoxLocalPosition;
        }
        else if (direction == Enums.PlayerDirection.Left)
        {
            // If starting to move left, ensure offsets are correct for the left-facing direction
            if (m_offset1.x > 0) m_offset1.x *= -1;
            if (m_groundCheckOffset.x > 0) m_groundCheckOffset.x *= -1;

            // Ensure the hitbox position is correct for the left-facing direction
            Vector3 hitBoxLocalPosition = m_playerHitBox.transform.localPosition;
            if (hitBoxLocalPosition.x > 0) hitBoxLocalPosition.x *= -1;
            m_playerHitBox.transform.localPosition = hitBoxLocalPosition;
        }

        // Apply the adjusted offsets to the colliders and ground check
        m_boxColliders[0].offset = m_offset1;
        m_playerMovement.groundCheckOffset = m_groundCheckOffset;
    }

}
