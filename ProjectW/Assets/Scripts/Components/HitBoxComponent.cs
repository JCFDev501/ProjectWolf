using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxComponent : MonoBehaviour
{
    [Header("Hit Box Settings")]
    // Indicates whether the hitbox should cause damage (true) or apply healing (false)
    [SerializeField] private bool m_isHarmful = true;

    // The amount of damage or healing this hitbox will apply when it collides with another object
    [SerializeField] private float m_effectValue = 0f;

    private bool wasHitBoxEnabled = false;

    public GameObject hitboxOwner = null;

    void OnEnable()
    {
        // Assuming the HitBoxComponent or the object it's attached to has a Collider2D
        Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(transform.position, 0.3f); // Adjust size/radius based on your needs

        foreach (var collider in overlappingColliders)
        {
            HealthComponent healthComponent = collider.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                 healthComponent.HandleHitBoxEnter(this);
                 wasHitBoxEnabled = true;
            }
        }
    }

    // Detects when another collider enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == gameObject.tag)
        {
            return;
        }

        if (!wasHitBoxEnabled)
        {
            HealthComponent healthComponent = other.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.HandleHitBoxEnter(this);
            }
        }

        wasHitBoxEnabled = false;

    }

    // This method destroys the game object to which this script is attached
    // It's separated into a method to keep the code organized and reusable
    public void DestroyGameObjectAttached()
    {
        Destroy(gameObject);
    }

    // This method returns whether the hitbox is harmful (causes damage) or not (causes healing)
    public bool IsHarmful()
    {
        return m_isHarmful;
    }

    // This method returns the amount of damage or healing that the hitbox applies
    public float EffectValue()
    {
        return m_effectValue;
    }
}


