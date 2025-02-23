using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttack : MonoBehaviour
{
    private PlayerController m_playerController; // Cached reference to the PlayerController script, used for input handling
    private GameObject m_playerHitBox; // The hitbox we are controlling
    private KaysAnimationManager m_Anim_Manager;

    private PlayerMovement m_playerMovement;

    [SerializeField] private float m_AttackDuration = 0.5f; // Duration for which the hitbox remains active

    // Start is called before the first frame update
    void Start()
    {
        // Cache the reference to the PlayerController script
        m_playerController = GetComponent<PlayerController>();
        if (m_playerController == null)
        {
            Debug.LogError("Could not find controller");
        }

        // Cache the player hitbox
        m_playerHitBox = gameObject.transform.GetChild(0).gameObject;
        if (m_playerHitBox == null)
        {
            Debug.LogError("Could not find HitBox");
        }
        else
        {
            m_playerHitBox.SetActive(false);
        }

        m_Anim_Manager = GetComponent<KaysAnimationManager>();
        if (m_Anim_Manager == null)
        {
            Debug.LogError("Could not find KaysAnimationManager");
        }

        m_playerMovement = GetComponent<PlayerMovement>();
        if (m_playerMovement == null)
        {
            Debug.LogError("Could not find PlayerMovment script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the attack button was pressed and if the player is not already attacking
        if (m_playerController.m_attackPressed && !m_playerController.m_isAttacking)
        {
            m_playerController.m_AttackStarted = true;

            if (m_playerMovement.m_playerLastDirection == Enums.PlayerDirection.Right)
            {
                m_Anim_Manager.PlayAnimation("Attack");
            }
            else if (m_playerMovement.m_playerLastDirection == Enums.PlayerDirection.Left)
            {
                // Added
                m_Anim_Manager.PlayAnimation("AttackLeft");
            }
            
        }
    }

    // Coroutine to handle the attack duration and resetting the hitbox
    private IEnumerator HandleAttack()
    {
        m_playerController.m_isAttacking = true; // Set the flag to indicate the player is attacking
        m_playerHitBox.SetActive(true); // Activate the hitbox

        // Wait for the duration of the attack
        yield return new WaitForSeconds(m_AttackDuration);

        m_playerHitBox.SetActive(false); // Deactivate the hitbox
        m_playerController.m_isAttacking = false; // Reset the flag to allow attacking again
        m_playerController.m_AttackStarted = false;

    }

    public void Attack()
    {
        StartCoroutine(HandleAttack());
    }
}

