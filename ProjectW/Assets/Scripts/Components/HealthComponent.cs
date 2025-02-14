using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Health Settings")]
    // The minimum health value this object can have (e.g., 0 for a dead state)
    [SerializeField] private float m_minHealth = 0f;

    // The maximum health value this object can have (e.g., 100 for full health)
    [SerializeField] private float m_maxHealth = 100f;

    // The current health value of the object
    [SerializeField] private float m_currentHealth = 100f;

    //Are we invulnerable
    [SerializeField] private bool m_invulnerable = false;

    // Start is called before the first frame update
    // Initializes the object's health to the maximum value when the game starts
    void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    // Todo: Temporary We need to implement some type of player manager script!
    // Update is called once per frame
    // Checks if the object is dead and, if so, logs a message and destroys the object
    void Update()
    {
        // If the object's health has reached the minimum value (i.e., it's dead)
        if (IsDead())
        {
            // Log the death to the console for debugging purposes
            Debug.Log(gameObject.name + " has died!");

            // Destroy the game object this script is attached to
            Destroy(gameObject);
        }
    }

    // Instantly heals the object by the specified value
    public void Heal(float val)
    {
        // Increase current health by the given value
        m_currentHealth += val;

        // Ensure that current health does not exceed the maximum health
        if (m_currentHealth > m_maxHealth)
            m_currentHealth = m_maxHealth;
    }

    // Instantly damages the object by the specified value
    public void TakeDamage(float val)
    {
        if (!m_invulnerable)
        {
            // Decrease current health by the given value
            m_currentHealth -= val;

            // Ensure that current health does not fall below the minimum health
            if (m_currentHealth < m_minHealth)
                m_currentHealth = m_minHealth;
        }

        
    }

    // Checks if the object is dead (i.e., current health is at or below the minimum health)
    public bool IsDead()
    {
        // Return true if the current health is less than or equal to the minimum health
        if (m_currentHealth <= m_minHealth)
            return true;

        // Otherwise, return false
        return false;
    }

    public void HandleHitBoxEnter(HitBoxComponent hitBox)
    {
        // Ensure the hitbox does not belong to the same game object
        if (hitBox.hitboxOwner == this.gameObject)
        {
            return; // Ignore the hitbox if it belongs to this object
        }

        // Check if the hitbox is harmful (causes damage)
        if (hitBox.IsHarmful())
        {
            // Apply damage to this object based on the hitbox's effect value
            TakeDamage(hitBox.EffectValue());
        }
        else
        {
            // Otherwise, heal this object based on the hitbox's effect value
            Heal(hitBox.EffectValue());
        }



    }

    public void IsInvulnerable(bool val)
    {
        m_invulnerable = val;
    }

}

