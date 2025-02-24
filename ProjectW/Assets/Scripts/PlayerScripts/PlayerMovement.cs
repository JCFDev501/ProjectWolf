using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //------------------------- [Tunable Vars] --------------------------------------------------------
    [SerializeField] private float speed = 5f; // Horizontal movement speed of the player
    [SerializeField] private float jumpForce = 10f; // Initial upward force applied when jumping
    [SerializeField] private float airControl = 0.5f; // Percentage of movement control retained while airborne
    [SerializeField] private float maxJumpTime = 0.5f; // Maximum time the jump button can be held to influence jump height
    [SerializeField] private float fallMultiplier = 2.5f; // Multiplier to increase fall speed for more responsive jumping
    [SerializeField] private float lowJumpMultiplier = 2f; // Multiplier for reduced jump height when jump button is released early

    [SerializeField] private Transform m_groundCheck; // Transform used as the origin point for ground detection
    [SerializeField] private float m_GroundDistance = 0.4f; // Radius around the ground check point for detecting ground
    [SerializeField] private LayerMask m_groundMask; // Layer(s) that represent ground objects for collision detection
    [SerializeField] public Vector2 groundCheckOffset; // Offset applied to the ground check position to fine-tune detection

    //------------------------- [Ref Vars] --------------------------------------------------------
    private Rigidbody2D rb; // Cached reference to the Rigidbody2D component, used for physics calculations
    private PlayerController playerController; // Cached reference to the PlayerController script, used for input handling
    private KaysAnimationManager m_kaysManager; // The Animation manager attached

    //------------------------- [Other Vars] --------------------------------------------------------
    [SerializeField] private bool isGrounded; // Boolean flag indicating if the player is currently grounded
    [SerializeField] private bool m_isFalling;  // Boolean flag indicating if the player is currently falling (moving downward)
    private bool jumpRequest; // Boolean flag to indicate if a jump was requested (used to handle input timing)
    private float jumpTimeCounter; // Timer to track how long the jump button has been held down
    private bool isJumping; // Boolean flag indicating if the player is currently in the process of jumping
    private bool stopMomentum; // Flag to temporarily stop the player's momentum, used after collisions

    //------------------------- [Player's Direction] -------------------------------------------------
    [SerializeField] public Enums.PlayerDirection m_playerDirection; // Enum to track the player's facing direction (left, right, none)
    [SerializeField] public Enums.PlayerDirection m_playerLastDirection; // Enum to track the player's facing direction (left, right, none)

    private BounceBack mBounceBack = null;

    // Start is called before the first frame update
    void Start()
    {
        // Cache the Rigidbody2D and PlayerController components for quick access
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();

        // Ensure the player has no drag, allowing for consistent movement based on input
        rb.drag = 0f;

        m_kaysManager = GetComponent<KaysAnimationManager>();

        mBounceBack = GetComponent<BounceBack>();

        if (!mBounceBack)
        {
            Debug.LogError("Cant find BounceBack Component");
        }
    }

    // Update is called once per frame, handling player input and state updates
    void Update()
    {

        if (mBounceBack.m_IsBouncingBack)
        {
            return;
        }
        
        // Check if the player is grounded by detecting collisions with ground objects
        Vector2 groundCheckPosition = m_groundCheck.position + (Vector3)groundCheckOffset;
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition, m_GroundDistance, m_groundMask);

        if (isGrounded)
        {
            stopMomentum = false;  // Allow movement to continue if grounded
            m_isFalling = false;   // Player is not falling if grounded
        }
        else if (rb.velocity.y < 0)
        {
            m_isFalling = true;    // Player is falling if not grounded and moving downward
        }
        else
        {
            m_isFalling = false;   // Player is airborne but not falling (e.g., rising during a jump)
        }

        // Determine the direction of the player based on the Rigidbody2D's velocity
        if (rb.velocity.x > 0)
        {
            m_playerDirection = Enums.PlayerDirection.Right; // Moving right
            m_playerLastDirection = Enums.PlayerDirection.Right;
        }
        else if (rb.velocity.x < 0)
        {
            m_playerDirection = Enums.PlayerDirection.Left;  // Moving left
            m_playerLastDirection = Enums.PlayerDirection.Left;
        }
        else
        {
            m_playerDirection = Enums.PlayerDirection.None;  // Not moving horizontally
        }

        // Handle jump initiation when the jump button is pressed and the player is grounded
        if (playerController.jumpPressed && isGrounded)
        {
            m_kaysManager.TriggerJump(); // Triggers jumping
            isJumping = true;  // Begin the jump process
            jumpTimeCounter = maxJumpTime;  // Reset the jump timer to the max jump time
            jumpRequest = true;  // Flag the jump request
        }

        // Handle continued jumping while the jump button is held
        if (playerController.jumpHeld && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                jumpTimeCounter -= Time.deltaTime;  // Decrease the jump timer
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // Continue applying upward force
            }
            else
            {
                isJumping = false;  // Stop jumping if the max jump time has been reached
            }
        }

        // Stop jumping if the jump button is released
        if (playerController.jumpReleased)
        {
            isJumping = false;
        }

        m_kaysManager.m_anim.SetBool("isGrounded", isGrounded);
        m_kaysManager.m_anim.SetBool("isFalling", m_isFalling);
    }

    // FixedUpdate is called at a fixed interval and is used for physics calculations
    void FixedUpdate()
    {
        if (mBounceBack.m_IsBouncingBack)
        {
            return;
        }
        
        if (!stopMomentum) // Only apply movement if momentum is not stopped by a collision
        {
            // Calculate horizontal movement based on player input
            float horizontalVelocity = playerController.getDirection * speed;

            // Apply reduced control in the air by decreasing horizontal velocity
            if (!isGrounded)
            {
                horizontalVelocity *= airControl;
            }

            // Smoothly transition the horizontal velocity and apply it to the Rigidbody2D
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, horizontalVelocity, isGrounded ? 1f : airControl), rb.velocity.y);

            // Apply additional downward force when falling to make the fall feel more responsive
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            // Apply a lower upward force if the jump button is released early for shorter jumps
            else if (!playerController.jumpHeld && rb.velocity.y > 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    // Handle collision events, specifically when colliding with static solid objects
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "StaticSolidObject")
        {
            // If the player is airborne and collides with something, stop their momentum
            if (!isGrounded)
            {
                stopMomentum = true;  // Stop all movement
                rb.velocity = Vector2.zero;  // Reset velocity to zero
            }
        }
    }

    // Draw a visual representation of the ground check radius in the editor for easier debugging
    void OnDrawGizmos()
    {
        if (m_groundCheck != null)
        {
            Gizmos.color = Color.red;
            Vector2 groundCheckPosition = m_groundCheck.position + (Vector3)groundCheckOffset;
            Gizmos.DrawWireSphere(groundCheckPosition, m_GroundDistance);  // Draw a wireframe sphere to represent the ground check area
        }
    }

    // Return the player's current direction (left, right, none)
    public Enums.PlayerDirection GetPlayerDirection()
    {
        return m_playerDirection;
    }

    // Return whether the player is grounded
    public bool IsGrounded()
    {
        return isGrounded;
    }

    // Return whether the player is currently falling
    public bool IsFalling()
    {
        return m_isFalling;
    }
}




