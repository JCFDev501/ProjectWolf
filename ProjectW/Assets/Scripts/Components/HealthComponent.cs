using UnityEngine;

/// <summary>
/// Manages the health system of a game object, including healing, taking damage, 
/// and invulnerability states. Ensures health stays within defined limits.
/// </summary>
public class HealthComponent : MonoBehaviour
{
    [Header("Health Settings")]
    
    [SerializeField] private float m_minHealth = 0f; // Minimum possible health value (e.g., 0 for death)
    [SerializeField] private float m_maxHealth = 100f; // Maximum possible health value
    [SerializeField] private float m_currentHealth = 100f; // Current health of the object
    [SerializeField] private bool m_invulnerable = false; // Determines if the object is immune to damage

    /// <summary>
    /// Initializes the object's health to the maximum value at the start of the game.
    /// </summary>
    void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    /// <summary>
    /// Heals the object by a specified amount without exceeding the max health.
    /// </summary>
    /// <param name="val">The amount to heal.</param>
    public void Heal(float val)
    {
        // Increase current health, ensuring it doesn't exceed the max health
        m_currentHealth = Mathf.Min(m_currentHealth + val, m_maxHealth);
    }

    /// <summary>
    /// Damages the object by a specified amount, unless invulnerable.
    /// Ensures health doesn't drop below the minimum health.
    /// </summary>
    /// <param name="val">The amount of damage taken.</param>
    public void TakeDamage(float val)
    {
        if (!m_invulnerable)
        {
            // Reduce health, ensuring it doesn't go below min health
            m_currentHealth = Mathf.Max(m_currentHealth - val, m_minHealth);
        }
    }

    /// <summary>
    /// Checks if the object's health is at or below the minimum, indicating death.
    /// </summary>
    /// <returns>True if the object is dead, otherwise false.</returns>
    public bool IsDead()
    {
        return m_currentHealth <= m_minHealth;
    }

    /// <summary>
    /// Sets the object's invulnerability state.
    /// </summary>
    /// <param name="val">True to make the object invulnerable, false to allow damage.</param>
    public void IsInvulnerable(bool val)
    {
        m_invulnerable = val;
    }
}


