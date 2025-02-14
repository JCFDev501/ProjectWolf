using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float getDirection { get; private set; }
    public bool jumpPressed { get; private set; }
    public bool m_attackPressed { get; private set; }
    public bool jumpHeld { get; private set; } // Property to check if the jump button is held
    public bool jumpReleased { get; private set; } // Property to check if the jump button is released

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal input (-1 for left, 1 for right, 0 for no input)
        getDirection = Input.GetAxisRaw("Horizontal");

        // Check if the jump button is pressed (default is "space")
        jumpPressed = Input.GetButtonDown("Jump");

        // Check if the Basic Attack Button was pressed
        m_attackPressed = Input.GetButtonDown("Basic Attack");

        // Check if the jump button is held
        jumpHeld = Input.GetButton("Jump");

        // Check if the jump button is released
        jumpReleased = Input.GetButtonUp("Jump");
    }
}

