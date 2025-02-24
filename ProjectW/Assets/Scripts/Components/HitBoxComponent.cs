using UnityEngine;

/// <summary>
/// Handles hitbox interactions, detecting collisions and triggering events based on
/// whether the hitbox is harmful (causes damage) or beneficial (heals).
/// </summary>
public class HitBoxComponent : MonoBehaviour
{
    [Header("Hit Box Settings")]

    [SerializeField] private bool m_isHarmful = true; // Determines if the hitbox causes damage (true) or healing (false)
    [SerializeField] private float m_effectValue = 0f; // The amount of damage or healing applied upon collision

    /// <summary>
    /// Called when the hitbox is enabled. Checks for immediate overlapping objects 
    /// and applies effects if necessary.
    /// </summary>
    void OnEnable()
    {
        Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);

        foreach (var collider in overlappingColliders)
        {
            GameObject target = collider.gameObject;

            // Ensure the hitbox has a parent object and avoid hitting its own parent
            if (transform.parent && target != null && target != transform.parent.gameObject)
            {
                GameObjectStateManager targetStateManager = target.GetComponent<GameObjectStateManager>();

                if (targetStateManager != null)
                {
                    GameObject thisHitBoxOwner = transform.parent.gameObject;

                    Debug.Log(thisHitBoxOwner.name + " Hit " + target.name);

                    // Create and add a new state event to the target's state manager
                    StateEvent newEvent = new StateEvent(thisHitBoxOwner, target, Enums.StateEventType.HitBoxEvent);
                    targetStateManager.AddStateEvent(newEvent);
                }
            }
        }
    }

    /// <summary>
    /// Detects when another collider enters the hitbox's trigger area, triggering hit events.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore interactions with static solid objects or other player hitboxes
        if (other.gameObject.CompareTag("StaticSolidObject") || other.gameObject.CompareTag("PlayerHitBox"))
        {
            return;
        }

        // Handle interaction with a player object
        if (other.gameObject.CompareTag("Player"))
        {
            GameObjectStateManager playerStateManager = other.GetComponent<GameObjectStateManager>();

            if (playerStateManager)
            {
                StateEvent newEvent = new StateEvent(gameObject, other.gameObject, Enums.StateEventType.HitBoxEvent);
                playerStateManager.AddStateEvent(newEvent);
            }
        }

        // Apply hit effects to the current object if it has a state manager
        GameObjectStateManager stateManager = GetComponent<GameObjectStateManager>();

        if (stateManager)
        {
            Debug.Log(gameObject.name + " Was Hit By " + other.name);

            StateEvent newEvent = new StateEvent(other.gameObject, gameObject, Enums.StateEventType.HitBoxEvent);
            stateManager.AddStateEvent(newEvent);
        }
    }

    /// <summary>
    /// Determines whether the hitbox is harmful (causes damage) or beneficial (heals).
    /// </summary>
    /// <returns>True if the hitbox causes damage, false if it provides healing.</returns>
    public bool IsHarmful()
    {
        return m_isHarmful;
    }

    /// <summary>
    /// Returns the effect value of the hitbox (amount of damage or healing applied).
    /// </summary>
    /// <returns>Effect value of the hitbox.</returns>
    public float EffectValue()
    {
        return m_effectValue;
    }
}
