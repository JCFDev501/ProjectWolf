using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Projectile class represents a basic projectile with properties for speed, lifespan, damage, and applied force.
/// It utilizes Rigidbody2D for physics interactions and automatically destroys itself after a set time or upon impact.
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField] private float mSpeed = 5f; // How fast the projectile moves
    [SerializeField] private float mTimeTillDestroy = 3f; // How long before the projectile is destroyed
    [SerializeField] private float mDamage = 10f; // How much damage the projectile deals
    [SerializeField] private float mAppliedForce = 5f; // How much force it applies to hit objects
    [SerializeField] public Vector2 mPositionOfWhoShotThis = new(0, 0); // position of who shot this

    private Rigidbody2D _rb = null;
    protected GameObject MWhatWasHit = null;
    protected Vector2 Direction; // Stores the movement direction

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (!_rb)
        {
            Debug.LogError("Can't find Rigidbody2D");
        }
        
        // Start the coroutine to destroy the projectile after a set time
        StartCoroutine(DestroyAfterTime());
    }

    private void FixedUpdate()
    {
        MoveProjectile();
    }

    /// <summary>
    /// Moves the projectile in its determined direction.
    /// This method is virtual so that derived classes can override it for custom movement behavior.
    /// </summary>
    protected virtual void MoveProjectile()
    {
        transform.position += (Vector3)Direction * (mSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Waits for the specified lifespan duration before destroying the projectile.
    /// </summary>
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(mTimeTillDestroy);
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles collision with other objects, storing the reference and triggering OnImpact.
    /// The projectile is destroyed immediately upon impact.
    /// </summary>
    /// <param name="other">The Collision2D data of the object hit.</param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Set what we hit
        MWhatWasHit = other.gameObject;
    }

    /// <summary>
    /// Called when the projectile impacts another object. 
    /// This method can be overridden in derived classes for custom behavior.
    /// </summary>
    public virtual void OnImpact()
    {
        Debug.Log("Do what we want then obj gets destoryed");
        
        // Destroy the projectile immediately after impact
        Destroy(gameObject);
    }
    
    public virtual void OnSpawn(GameObject whoShotThis)
    {
        //mPositionOfWhoShotThis = whoShotThis.transform.position;
        //
        //// Set the direction based on the object's local right direction
        //Direction = transform.right.normalized;
    }
}

