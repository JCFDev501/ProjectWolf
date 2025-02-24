using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Handles the player's shooting mechanics, including firing projectiles,
/// managing animation states, and enforcing a cooldown between shots.
/// </summary>
public class PlayerBasicShoot : MonoBehaviour
{
    private PlayerController _mPlayerController; // Cached reference to the PlayerController script for input handling
    private KaysAnimationManager _mAnimManager; // Reference to animation manager for handling shooting animations
    private PlayerMovement _mPlayerMovement; // Reference to track player's movement direction
    
    [SerializeField] private float mTimeBetweenShots = 0.5f; // Cooldown time between shots
    [FormerlySerializedAs("_mProjectileToSpawn")] [SerializeField] private GameObject mProjectileToSpawn; // Projectile prefab to spawn
    [SerializeField] private GameObject mProjectileSpawnPoint = null; // Position where the projectile spawns

    private bool _mReadyToFire = true; // Flag to track if the player can shoot
    
    /// <summary>
    /// Initializes references and ensures all necessary components are set up.
    /// </summary>
    void Start()
    {
        // Cache the reference to the PlayerController script
        _mPlayerController = GetComponent<PlayerController>();
        if (_mPlayerController == null)
        {
            Debug.LogError("Could not find PlayerController.");
        }
        
        if (mProjectileToSpawn == null)
        {
            Debug.LogError("Projectile prefab is not assigned.");
        }
        
        _mAnimManager = GetComponent<KaysAnimationManager>();
        if (_mAnimManager == null)
        {
            Debug.LogError("Could not find KaysAnimationManager.");
        }

        _mPlayerMovement = GetComponent<PlayerMovement>();
        if (_mPlayerMovement == null)
        {
            Debug.LogError("Could not find PlayerMovement script.");
        }
    }

    /// <summary>
    /// Monitors input and triggers shooting when the shoot button is pressed.
    /// </summary>
    void Update()
    {
        // Check if the shoot button is pressed and the player is ready to fire
        if (_mPlayerController.m_shootPressed && _mReadyToFire)
        {
            StartCoroutine(ShootCooldown());
        }
    }

    /// <summary>
    /// Coroutine to handle the shooting cooldown, preventing rapid fire.
    /// </summary>
    private IEnumerator ShootCooldown()
    {
        // Execute shoot logic
        Shoot();

        // Disable shooting until cooldown completes
        _mReadyToFire = false;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(mTimeBetweenShots);

        // Enable shooting again
        _mReadyToFire = true;
    }

    /// <summary>
    /// Instantiates and fires a projectile from the player's shooting position.
    /// </summary>
    private void Shoot()
    {
        Debug.Log("Player shot a projectile!");
        
        // Spawn the projectile at the defined spawn point position and rotation
        GameObject newProjectile = Instantiate(mProjectileToSpawn, mProjectileSpawnPoint.transform.position, mProjectileSpawnPoint.transform.rotation);

        // Initialize the projectile with reference to the shooter
        newProjectile.GetComponent<Projectile>().OnSpawn(gameObject);
    }
}

