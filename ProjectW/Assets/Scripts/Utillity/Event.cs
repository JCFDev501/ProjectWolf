using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StateEvent
{
    public GameObject initiator;
    public GameObject target;
    public Enums.StateEventType eventType;

    public StateEvent(GameObject initiator, GameObject target, Enums.StateEventType eventType)
    {
        this.initiator = initiator;
        this.target = target;
        this.eventType = eventType;
    }
}
