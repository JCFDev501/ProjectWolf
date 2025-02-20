using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyEventHandler : MonoBehaviour
{

    public void HandleEvents(StateEvent eventToHandle)
    {
        Debug.Log(gameObject.name + " processing event: " + eventToHandle.eventType);

        GetComponent<BounceBack>().ApplyBounceBack(eventToHandle.initiator.gameObject.transform.position);
    }
}
