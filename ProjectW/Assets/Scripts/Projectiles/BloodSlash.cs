using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSlash : Projectile
{
    public override void OnImpact()
    {
        base.OnImpact();
    }

    public override void OnSpawn(GameObject whoShotThis)
    {
        mPositionOfWhoShotThis = whoShotThis.transform.position;
    
        // Get the PlayerMovement component to determine the direction
        PlayerMovement pm = whoShotThis.GetComponent<PlayerMovement>();

        if (pm != null)
        {
            if (pm.m_playerLastDirection == Enums.PlayerDirection.Right)
            {
                Direction = transform.right.normalized;  // Move to the right
            }
            else if (pm.m_playerLastDirection == Enums.PlayerDirection.Left)
            {
                Direction = -transform.right.normalized; // Move to the left
            }
        }
        else
        {
            Debug.LogError("PlayerMovement component not found on the shooter!");
        }
    }

}
