using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventHandler : MonoBehaviour
{
    public void HandleEvents(StateEvent eventToHandle)
    {
        Debug.Log(gameObject.name + " processing event: " + eventToHandle.eventType);

        if (eventToHandle.initiator == eventToHandle.target)
        {
            return;
        }
        
        // If our event is a hit box event
        if (eventToHandle.eventType == Enums.StateEventType.HitBoxEvent)
        {
            // If the initiator is a projectile, we need to find its origin point
            if (eventToHandle.initiator.CompareTag("Projectile"))
            {
                Projectile projectileToLookAt = eventToHandle.initiator.GetComponent<Projectile>();

                if (projectileToLookAt)
                {
                    GetComponent<BounceBack>().ApplyBounceBack(projectileToLookAt.mPositionOfWhoShotThis);
                    
                    projectileToLookAt.OnImpact();
                    
                    return;
                }
            }
        }

        GetComponent<BounceBack>().ApplyBounceBack(eventToHandle.initiator.gameObject.transform.position);
    }
}
