using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 moveDirection = Vector2.right; // Set direction in the Editor

    private Rigidbody2D rb;
    private BounceBack bb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bb = GetComponent<BounceBack>();
    }

    void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (rb == null) 
            return;

        if (bb.m_IsBouncingBack)
            return;

        // Ensure movement is strictly horizontal or vertical
        if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
        {
            moveDirection.y = 0; // Remove diagonal influence
            moveDirection.x = Mathf.Sign(moveDirection.x); // Force strict left/right
        }
        else
        {
            moveDirection.x = 0; // Remove diagonal influence
            moveDirection.y = Mathf.Sign(moveDirection.y); // Force strict up/down
        }

        // Apply movement using velocity
        rb.velocity = moveDirection * moveSpeed;
    }
}
