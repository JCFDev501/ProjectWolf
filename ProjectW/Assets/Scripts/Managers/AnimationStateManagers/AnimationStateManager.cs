using UnityEngine;

/// <summary>
/// Manages the animation state transitions for an object using an Animator.
/// This script allows smooth transitioning between animation states.
/// </summary>
public class AnimationStateManager : MonoBehaviour
{
    [Header("Animation State Manager")]
    public Animator m_anim; // Reference to the Animator component.
    protected BaseAnimState m_currentState; // The current animation state.

    /// <summary>
    /// Initializes the Animator and sets the default state to null.
    /// </summary>
    protected void Start()
    {
        // Cache the Animator component
        m_anim = GetComponent<Animator>();

        if (m_anim == null)
        {
            Debug.LogWarning("No Animator found on this object!"); // Logs a warning if no Animator is attached.
        }

        m_currentState = null; // Default state is null.
    }

    /// <summary>
    /// Updates the current animation state every frame.
    /// </summary>
    protected void Update()
    {
        // If there is a current state, update it
        m_currentState?.UpdateState(this);
    }

    /// <summary>
    /// Transitions to a new animation state.
    /// Handles exiting the previous state and entering the new one.
    /// </summary>
    /// <param name="newState">The new animation state to transition to.</param>
    public void TransitionToState(BaseAnimState newState)
    {
        if (newState == null) return; // Safety check to prevent null transitions.

        // Exit the current state, if any
        m_currentState?.ExitState(this);

        // Assign and enter the new state
        m_currentState = newState;
        m_currentState.EnterState(this);
    }
}

