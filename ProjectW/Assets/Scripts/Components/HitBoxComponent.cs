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

    void OnEnable()
    {
        Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);

        foreach (var collider in overlappingColliders)
        {
            GameObject target = collider.gameObject;

            if (target != null && target != gameObject)
            {
                GameObjectStateManager targetStateManager = target.GetComponent<GameObjectStateManager>();

                if (targetStateManager != null)
                {
                    GameObject thisHitBoxOwner = gameObject.transform.parent.gameObject;

                    Debug.Log(thisHitBoxOwner.name + " Hit " + target.name);

                    StateEvent newEvent = new StateEvent(thisHitBoxOwner.gameObject, target,
                        Enums.StateEventType.HitBoxEvent);

                    targetStateManager.AddStateEvent(newEvent);
                    
                }
            }
        }
    }

    // Detects when another collider enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        //
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


