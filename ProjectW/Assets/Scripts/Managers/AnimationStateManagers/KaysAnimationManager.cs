using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaysAnimationManager : AnimationStateManager
{
    public PlayerController m_playerController; // Cached reference to the PlayerController script

    public KayWalkingRight KayWalkingRightState = new KayWalkingRight();
    public KayWalkingLeft KayWalkingLeftState = new KayWalkingLeft();
    public KayIdleLeft KayIdleLeftState = new KayIdleLeft();
    public KayIdleRight KayIdleRightState = new KayIdleRight();

    // Start is called before the first frame update
    new void Start()
    {
        // Cache the reference to the PlayerController script
        m_playerController = GetComponent<PlayerController>();
        if (m_playerController == null)
        {
            Debug.LogError("Could not find controller");
        }

        base.Start(); // Calls the Start method of the base class (AnimationStateManager)

        m_currentState = KayIdleRightState;

        // Enter the new state...
        m_currentState.EnterState(this);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update(); // Calls the Update method of the base class (AnimationStateManager)
    }

    // Play A Certain animation
    public void PlayAnimation(string animationName)
    {
        m_anim.Play(animationName);
    }

    public void TriggerJump()
    {
        m_anim.SetTrigger("JUMP");
    }
}
