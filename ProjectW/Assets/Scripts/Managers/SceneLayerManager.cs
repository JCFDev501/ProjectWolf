using System.Collections.Generic;
using UnityEngine;
using static Enums;

[ExecuteAlways] // Ensures this script runs in both the Unity Editor and at runtime
public class SceneLayerManager : MonoBehaviour
{
    [Header("Layer Settings")]
    [SerializeField] private LayerGroup m_layerGroup;  // The layer group assigned to this GameObject

    /// <summary>
    /// Dictionary mapping each LayerGroup to its corresponding Z-axis position.
    /// This helps organize the scene layers in a structured manner.
    /// </summary>
    private readonly Dictionary<LayerGroup, float> m_layerZPositions = new Dictionary<LayerGroup, float>
    {
        { LayerGroup.BackGround, 5f },         // Farthest back layer
        { LayerGroup.BackMiddleGround, 4f },
        { LayerGroup.MiddleGround, 3f },
        { LayerGroup.MiddleForeGround, 2f },
        { LayerGroup.PreForeGround, 1f },
        { LayerGroup.ForeGround, 0f },         // Default layer for most objects
        { LayerGroup.PreEffectLayer, -1f },
        { LayerGroup.EffectLayer, -2f },
        { LayerGroup.Ui, -3f },                // UI elements closer to the front
        { LayerGroup.Overlay, -4f }            // Foremost overlay layer
    };

    /// <summary>
    /// Called whenever a value is changed in the Inspector.
    /// Ensures the Z position updates automatically when modified in the Unity Editor.
    /// </summary>
    void OnValidate()
    {
        SetZPosition();
    }

    /// <summary>
    /// Called when the script starts.
    /// Ensures the object is positioned correctly on the Z-axis based on its assigned layer.
    /// </summary>
    void Start()
    {
        SetZPosition();
    }

    /// <summary>
    /// Sets the Z position of the GameObject based on the selected LayerGroup.
    /// If the LayerGroup is not found in the dictionary, an error message is logged.
    /// </summary>
    private void SetZPosition()
    {
        if (m_layerZPositions.TryGetValue(m_layerGroup, out float zPosition))
        {
            Vector3 newPosition = transform.position;
            newPosition.z = zPosition;
            transform.position = newPosition;
        }
        else
        {
            Debug.LogError($"LayerGroup '{m_layerGroup}' not recognized! Ensure it is defined in the dictionary.");
        }
    }
}

