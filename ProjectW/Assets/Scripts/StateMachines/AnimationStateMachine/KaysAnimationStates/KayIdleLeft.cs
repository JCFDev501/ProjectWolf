using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayIdleLeft : BaseAnimState
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
        else
            m_kaysManager.PlayAnimation("Kay_Idle_Left");

    }

    public override void UpdateState(AnimationStateManager manager)
    {
        if (m_playerMovement.GetPlayerDirection() == Enums.PlayerDirection.Right)
            m_kaysManager.TransitionToState(m_kaysManager.KayWalkingRightState);
        else if (m_playerMovement.GetPlayerDirection() == Enums.PlayerDirection.Left)
            m_kaysManager.TransitionToState(m_kaysManager.KayWalkingLeftState);
    }

    public override void ExitState(AnimationStateManager manager)
    {
        //
    }
}
