using UnityEngine;

/// <summary>
/// Represents an item in the game world that a player can collect.
/// The item type determines its behavior when collected.
/// </summary>
public class Item : MonoBehaviour
{
    [Header("Item Settings")]
    [SerializeField] private Enums.ItemType itemType; // Defines the type of item (Collectable, Key, Tool, etc.)

    /// <summary>
    /// Called when the item is collected by the player.
    /// Destroys the item if it is of a valid type.
    /// </summary>
    public void OnPlayerCollect()
    {
        // Ignore collection if the item type is "None"
        if (itemType == Enums.ItemType.None)
        {
            return;
        }

        // Log and destroy the item based on its type
        Debug.Log(gameObject.name + " was collected and destroyed.");
        Destroy(gameObject);
    }
}
