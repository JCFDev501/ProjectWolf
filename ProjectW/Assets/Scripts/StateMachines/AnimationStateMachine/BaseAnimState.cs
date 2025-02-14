using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Class for all animation states
public abstract class BaseAnimState
{
    public abstract void EnterState(AnimationStateManager manager); // When we enter the state.
    public abstract void UpdateState(AnimationStateManager manager); // While we are in the state.
    public abstract void ExitState(AnimationStateManager manager); // When we leave the state.
}
