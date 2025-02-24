using UnityEngine;

/// <summary>
/// Manages Kay's animation states and transitions based on player input.
/// Inherits from AnimationStateManager and handles different animation states.
/// </summary>
public class KaysAnimationManager : AnimationStateManager
{
    [Header("Player References")]
    public PlayerController m_playerController; // Cached reference to the PlayerController script.

    [Header("Animation States")]
    public readonly KayWalkingRight KayWalkingRightState = new KayWalkingRight();
    public readonly KayWalkingLeft KayWalkingLeftState = new KayWalkingLeft();
    public readonly KayIdleLeft KayIdleLeftState = new KayIdleLeft();
    public readonly KayIdleRight KayIdleRightState = new KayIdleRight();

    /// <summary>
    /// Initializes the animation manager, assigns default state, and ensures dependencies are set up.
    /// </summary>
    new void Start()
    {
        // Cache the reference to the PlayerController script
        m_playerController = GetComponent<PlayerController>();
        if (m_playerController == null)
        {
            Debug.LogError("PlayerController not found on this GameObject.");
        }

        // Call the base class's Start method
        base.Start();

        // Set the default animation state to idle facing right
        m_currentState = KayIdleRightState;
        m_currentState.EnterState(this); // Trigger initial state entry logic
    }

    /// <summary>
    /// Updates the current animation state each frame.
    /// </summary>
    new void Update()
    {
        base.Update(); // Calls the base class's Update method to manage state transitions.
    }

    /// <summary>
    /// Plays an animation by name.
    /// </summary>
    /// <param name="animationName">The name of the animation to play.</param>
    public void PlayAnimation(string animationName)
    {
        if (m_anim != null)
        {
            m_anim.Play(animationName);
        }
        else
        {
            Debug.LogWarning("Animator component not assigned.");
        }
    }

    /// <summary>
    /// Triggers the jump animation by setting the "JUMP" trigger in the Animator.
    /// </summary>
    public void TriggerJump()
    {
        if (m_anim != null)
        {
            m_anim.SetTrigger("JUMP");
        }
        else
        {
            Debug.LogWarning("Animator component not assigned.");
        }
    }
}

