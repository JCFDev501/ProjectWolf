using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventHandler : MonoBehaviour
{
    public void HandleEvents(StateEvent eventToHandle)
    {
        Debug.Log(gameObject.name + " processing event: " + eventToHandle.eventType);
    }
}
