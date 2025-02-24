using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the player's basic attack logic, including hitbox activation,
/// animation handling, and cooldown management.
/// </summary>
public class PlayerBasicAttack : MonoBehaviour
{
    private PlayerController m_playerController; // Cached reference to PlayerController
    private KaysAnimationManager m_Anim_Manager;
    private PlayerMovement m_playerMovement;

    [SerializeField] private GameObject m_playerHitBox; // Player's attack hitbox (assign in Inspector)
    [SerializeField] private float m_AttackDuration = 0.5f; // Attack hitbox active duration

    void Start()
    {
        // Cache references
        m_playerController = GetComponent<PlayerController>();
        m_Anim_Manager = GetComponent<KaysAnimationManager>();
        m_playerMovement = GetComponent<PlayerMovement>();

        // Validate components
        if (m_playerController == null)
            Debug.LogError("PlayerController component not found on the player.");
        if (m_Anim_Manager == null)
            Debug.LogError("KaysAnimationManager component not found.");
        if (m_playerMovement == null)
            Debug.LogError("PlayerMovement component not found.");
        if (m_playerHitBox == null)
            Debug.LogError("Hitbox reference not assigned. Assign it in the Inspector.");
        else
            m_playerHitBox.SetActive(false); // Ensure hitbox starts disabled
    }

    void Update()
    {
        // Check if the attack button is pressed and if the player is not already attacking
        if (m_playerController.m_attackPressed && !m_playerController.m_isAttacking)
        {
            m_playerController.m_AttackStarted = true;

            // Play attack animation based on player's facing direction
            if (m_playerMovement.m_playerLastDirection == Enums.PlayerDirection.Right)
            {
                m_Anim_Manager.PlayAnimation("Attack");
            }
            else if (m_playerMovement.m_playerLastDirection == Enums.PlayerDirection.Left)
            {
                m_Anim_Manager.PlayAnimation("AttackLeft");
            }
        }
    }

    /// <summary>
    /// Initiates the attack by enabling the hitbox and managing its duration.
    /// </summary>
    public void Attack()
    {
        StartCoroutine(HandleAttack());
    }

    /// <summary>
    /// Coroutine that manages the attack duration and resets the hitbox after completion.
    /// </summary>
    private IEnumerator HandleAttack()
    {
        m_playerController.m_isAttacking = true; // Set attacking flag
        m_playerHitBox.SetActive(true); // Enable hitbox

        yield return new WaitForSeconds(m_AttackDuration); // Wait for attack duration

        m_playerHitBox.SetActive(false); // Disable hitbox
        m_playerController.m_isAttacking = false; // Reset attack flag
        m_playerController.m_AttackStarted = false; // Reset attack started flag
    }
}
