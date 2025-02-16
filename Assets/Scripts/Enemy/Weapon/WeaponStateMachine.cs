using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStateMachine
{
    // Start is called before the first frame update
    public WeaponState currentState { get; private set; }
    public WeaponState previousState { get; private set; }

    public void Initialize(WeaponState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(WeaponState _newState)
    {
        previousState = currentState;
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }

    public void ChangeToPreviousState()
    {
        currentState.Exit();
        currentState = previousState;
        currentState.Enter();
    }
}
