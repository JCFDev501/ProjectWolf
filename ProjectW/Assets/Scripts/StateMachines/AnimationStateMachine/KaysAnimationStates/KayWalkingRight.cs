using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayWalkingRight : BaseAnimState
{
    private PlayerMovement m_playerMovement;
    private KaysAnimationManager m_kaysManager;

    public override void EnterState(AnimationStateManager manager)
    {
        if (m_playerMovement == null)
            m_playerMovement = manager.GetComponent<PlayerMovement>();

        m_kaysManager = manager as KaysAnimationManager; // Caste to our child
        if (m_kaysManager == null)
            Debug.LogError("Manager is not of type PlayerAnimationManager");

        manager.m_anim.SetBool("isWalkingRight", true);
    }

    public override void UpdateState(AnimationStateManager manager)
    {
        m_kaysManager = manager as KaysAnimationManager; // Caste to our child

        if (m_kaysManager != null)
        {
            if (m_playerMovement.GetPlayerDirection() == Enums.PlayerDirection.Left)
                m_kaysManager.TransitionToState(m_kaysManager.KayWalkingLeftState);
            else if (m_playerMovement.GetPlayerDirection() == Enums.PlayerDirection.None && m_playerMovement.IsGrounded() && !m_kaysManager.m_playerController.m_AttackStarted)
                m_kaysManager.TransitionToState(m_kaysManager.KayIdleRightState);
        }
            
    }

    public override void ExitState(AnimationStateManager manager)
    {
        manager.m_anim.SetBool("isWalkingRight", false);
    }
}
