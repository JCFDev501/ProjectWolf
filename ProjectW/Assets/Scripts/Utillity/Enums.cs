using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum ItemType
    {
        None,
        Collectable,
        Key,
        Tool
    }

    public enum PlayerDirection
    {
        Right,
        Left,
        None
    }

    public enum LayerGroup
    {
        BackGround,         // Farthest layer background elements                                                                                            Z Axis --> 5
        BackMiddleGround,   // Layer between BackGround and MiddleGround for elements closer than BackGround but farther than MiddleGround                   Z Axis --> 4
        MiddleGround,       // Mid-range layer for non-interactive elements closer than the background                                                       Z Axis --> 3
        MiddleForeGround,   // Layer between MiddleGround and ForeGround for elements closer than MiddleGround but farther than ForeGround                   Z Axis --> 2
        PreForeGround,      // New layer before the ForeGround                                                                                               Z Axis --> 1
        ForeGround,         // Foreground layer for the player character and interactive elements                                                            Z Axis --> 0
        PreEffectLayer,     // New layer before the EffectLayer                                                                                              Z Axis --> -1
        EffectLayer,        // Layer for visual effects that appear in front of the ForeGround but behind the UI                                             Z Axis --> -2
        Ui,                 // User Interface layer for HUD, menus, and other overlay elements                                                               Z Axis --> -3
        Overlay,            // Overlay layer for full-screen effects or filters that need to be on top of everything                                         Z Axis --> -4

        // Camera Will Always Be at Z Axis -10
    }

    public enum StateEventType
    {
        HitBoxEvent,
    }

}
