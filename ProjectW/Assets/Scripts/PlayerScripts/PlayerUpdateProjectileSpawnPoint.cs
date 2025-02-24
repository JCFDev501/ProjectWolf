using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdateProjectileSpawnPoint : MonoBehaviour
{
    private PlayerMovement m_playerMovement;

    [SerializeField] private float m_spawnOffsetX = 1f; // Distance from the player to spawn the projectile

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to PlayerMovement script from the parent
        m_playerMovement = gameObject.transform.parent.GetComponent<PlayerMovement>();
        
        if (m_playerMovement == null)
        {
            Debug.LogError("Could not find PlayerMovement script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the spawn point is a certain distance from the player based on direction
        Vector3 spawnOffset = new Vector3(m_spawnOffsetX, 0, 0);

        if (m_playerMovement.m_playerLastDirection == Enums.PlayerDirection.Left)
        {
            // When moving left, move spawn point to the left of the player
            transform.position = m_playerMovement.transform.position - spawnOffset;
        }
        else if (m_playerMovement.m_playerLastDirection == Enums.PlayerDirection.Right)
        {
            // When moving right, move spawn point to the right of the player
            transform.position = m_playerMovement.transform.position + spawnOffset;
        }
    }
}


