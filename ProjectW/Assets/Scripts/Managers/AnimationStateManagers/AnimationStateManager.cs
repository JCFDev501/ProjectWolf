using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateManager : MonoBehaviour
{
    public Animator m_anim; // A ref to the Animator.
    protected BaseAnimState m_currentState; // The current state we are in.


    // Start is called before the first frame update
    protected void Start()
    {
        m_anim = GetComponent<Animator>(); // Cache the animator.
        if (m_anim == null )
            Debug.LogWarning("No animator found on this object!"); // Logs a warning message to Unity.

        m_currentState = null; // By Default our current state is null.
    }

    // Update is called once per frame
    protected void Update()
    {
        // Update the state...
        if (m_currentState != null)
            m_currentState.UpdateState(this);
    }

    // Change the state...
    public void TransitionToState(BaseAnimState newState)
    {
        // Exit the current state...
        if (m_currentState != null)
            m_currentState.ExitState(this);

        // Change the state...
        m_currentState = newState;

        // Enter the new state...
        m_currentState.EnterState(this);
    }
}
