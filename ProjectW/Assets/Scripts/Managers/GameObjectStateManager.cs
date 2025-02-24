using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages state events for a GameObject by queuing and processing them.
/// Uses an assigned event handler script to handle event execution.
/// </summary>
public class GameObjectStateManager : MonoBehaviour
{
    /// <summary>
    /// Queue holding state events to be processed.
    /// </summary>
    private Queue<StateEvent> m_StateEvents = new Queue<StateEvent>();

    [Header("Event Handling")]
    [Tooltip("Assign a script that implements the HandleEvents method.")]
    [SerializeField] private MonoBehaviour m_EventHandlerScript;

    /// <summary>
    /// Processes queued state events every frame.
    /// </summary>
    void Update()
    {
        if (m_StateEvents.Count > 0)
        {
            ProcessStateEvents();
        }
    }

    /// <summary>
    /// Processes all state events in the queue.
    /// </summary>
    private void ProcessStateEvents()
    {
        while (m_StateEvents.Count > 0)
        {
            StateEvent eventToProcess = m_StateEvents.Dequeue();

            if (m_EventHandlerScript != null)
            {
                m_EventHandlerScript.SendMessage("HandleEvents", eventToProcess, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                Debug.LogWarning($"{gameObject.name}: No event handler script assigned. State event ignored.");
            }
        }
    }

    /// <summary>
    /// Adds a new state event to the queue.
    /// </summary>
    /// <param name="newEvent">The state event to add.</param>
    public void AddStateEvent(StateEvent newEvent)
    {
        m_StateEvents.Enqueue(newEvent);
    }
}


