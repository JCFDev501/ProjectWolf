using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBack : MonoBehaviour
{
    [SerializeField] private float m_KnockbackForce = 5f;
    [SerializeField] private float m_LiftForce = 2f; // Force to lift the object slightly
    [SerializeField] private float m_BounceBackDuration = 0.2f;

    public bool m_IsBouncingBack = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyBounceBack(Vector2 sourcePosition)
    {
        if (rb == null) return; // Safety check

        // Calculate direction away from source
        Vector2 knockbackDirection = (Vector2)(transform.position - (Vector3)sourcePosition);
        knockbackDirection.Normalize(); // Ensure uniform movement

        // Ensure movement is strictly horizontal or vertical
        if (Mathf.Abs(knockbackDirection.x) > Mathf.Abs(knockbackDirection.y))
        {
            knockbackDirection.y = 0; // Remove diagonal influence
            knockbackDirection.x = Mathf.Sign(knockbackDirection.x); // Force strict left/right
        }
        else
        {
            knockbackDirection.x = 0; // Remove diagonal influence
            knockbackDirection.y = Mathf.Sign(knockbackDirection.y); // Force strict up/down
        }

        // Apply a quick lift before bouncing back
        rb.velocity = Vector2.zero; // Reset velocity to avoid stacking forces
        rb.AddForce(Vector2.up * m_LiftForce, ForceMode2D.Impulse); // Lift the object
        rb.AddForce(knockbackDirection * m_KnockbackForce, ForceMode2D.Impulse); // Knockback force

        StartCoroutine(HandleBounceBackDur());
    }

    // Coroutine to handle the attack duration and resetting the hitbox
    private IEnumerator HandleBounceBackDur()
    {
        m_IsBouncingBack = true;

        // Wait for the duration of the attack
        yield return new WaitForSeconds(m_BounceBackDuration);


        m_IsBouncingBack = false;
    }
}
