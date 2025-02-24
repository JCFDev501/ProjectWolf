using System.Collections;
using UnityEngine;

/// <summary>
/// This script applies a bounce-back effect when an object is hit.
/// It pushes the object away from the source position while ensuring movement remains strictly horizontal or vertical.
/// Additionally, a slight lift force is applied to create a more natural bounce effect.
/// </summary>
public class BounceBack : MonoBehaviour
{
    [SerializeField] private float m_KnockbackForce = 5f;  // Strength of the knockback force
    [SerializeField] private float m_LiftForce = 2f;       // Vertical lift applied before knockback
    [SerializeField] private float m_BounceBackDuration = 0.2f;  // Duration of the bounce-back effect

    public bool m_IsBouncingBack = false;  // Indicates if the object is currently bouncing back

    private Rigidbody2D rb;  // Reference to the object's Rigidbody2D component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Applies a bounce-back effect, pushing the object away from the source position.
    /// Ensures movement is restricted to strictly horizontal or vertical directions.
    /// </summary>
    /// <param name="sourcePosition">The position of the source causing the bounce-back effect.</param>
    public void ApplyBounceBack(Vector2 sourcePosition)
    {
        if (rb == null) return; // Safety check to ensure Rigidbody2D is assigned

        // Calculate direction away from the source position
        Vector2 knockbackDirection = (Vector2)(transform.position - (Vector3)sourcePosition);
        knockbackDirection.Normalize(); // Normalize to ensure consistent force application

        // Restrict movement to either horizontal or vertical
        if (Mathf.Abs(knockbackDirection.x) > Mathf.Abs(knockbackDirection.y))
        {
            knockbackDirection.y = 0;  // Eliminate diagonal influence, enforcing left/right movement
            knockbackDirection.x = Mathf.Sign(knockbackDirection.x);  // Force movement strictly left or right
        }
        else
        {
            knockbackDirection.x = 0;  // Eliminate diagonal influence, enforcing up/down movement
            knockbackDirection.y = Mathf.Sign(knockbackDirection.y);  // Force movement strictly up or down
        }

        // Reset velocity to prevent force stacking
        rb.velocity = Vector2.zero;

        // Apply a slight vertical lift before the knockback force
        rb.AddForce(Vector2.up * m_LiftForce, ForceMode2D.Impulse);
        rb.AddForce(knockbackDirection * m_KnockbackForce, ForceMode2D.Impulse);

        // Start coroutine to manage bounce-back duration
        StartCoroutine(HandleBounceBackDur());
    }

    /// <summary>
    /// Coroutine that manages the duration of the bounce-back effect.
    /// Prevents additional bounce-back actions until the effect duration is over.
    /// </summary>
    private IEnumerator HandleBounceBackDur()
    {
        m_IsBouncingBack = true;  // Mark as bouncing back

        yield return new WaitForSeconds(m_BounceBackDuration);  // Wait for the duration

        m_IsBouncingBack = false;  // Reset bounce-back state
    }
}
