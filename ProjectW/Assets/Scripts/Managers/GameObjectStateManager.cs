using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for state managers, providing event processing functionality.
/// </summary>
public class GameObjectStateManager : MonoBehaviour
{
    // Queue to process state events
    private Queue<StateEvent> m_StateEvents = new Queue<StateEvent>();

    [SerializeField] private MonoBehaviour m_EventHandlerScript; // Assignable script in Unity Editor

    void Update()
    {
        // Only process if there are events in the queue
        if (m_StateEvents.Count > 0)
        {
            ProcessStateEvents();
        }
    }

    private void ProcessStateEvents()
    {
        while (m_StateEvents.Count > 0)
        {
            StateEvent eventToProcess = m_StateEvents.Dequeue();

            // Call the method from the assigned script
            if (m_EventHandlerScript != null)
            {
                m_EventHandlerScript.SendMessage("HandleEvents", eventToProcess, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                Debug.LogWarning("No event handler script assigned!");
            }
        }
    }


    /// <summary>
    /// Public method to enqueue new state events.
    /// </summary>
    public void AddStateEvent(StateEvent newEvent)
    {
        m_StateEvents.Enqueue(newEvent);
    }
}

