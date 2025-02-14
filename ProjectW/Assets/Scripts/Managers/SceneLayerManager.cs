using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

[ExecuteAlways] // Ensure this script runs in the editor as well as at runtime
public class SceneLayerManager : MonoBehaviour
{
    [Header("Layer")]
    [SerializeField] private LayerGroup m_layerGroup;  // The layer group to assign to this GameObject

    // Dictionary to map LayerGroup to Z-axis values
    private readonly Dictionary<LayerGroup, float> m_layerZPositions = new Dictionary<LayerGroup, float>
    {
        { LayerGroup.BackGround, 5f },
        { LayerGroup.BackMiddleGround, 4f },
        { LayerGroup.MiddleGround, 3f },
        { LayerGroup.MiddleForeGround, 2f },
        { LayerGroup.PreForeGround, 1f },   
        { LayerGroup.ForeGround, 0f },
        { LayerGroup.PreEffectLayer, -1f },
        { LayerGroup.EffectLayer, -2f },
        { LayerGroup.Ui, -3f },
        { LayerGroup.Overlay, -4f }
    };

    // This method is called whenever the script is loaded or a value is changed in the Inspector
    void OnValidate()
    {
        SetZPosition();
    }

    // This method is called when the script starts
    void Start()
    {
        SetZPosition();
    }

    // Method to set the Z position based on the selected LayerGroup
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
            Debug.LogError("LayerGroup not recognized!");
        }
    }
}
