using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowStateMachine
{
    public ArrowState currentState { get; private set; }
    public ArrowState previousState { get; private set; }

    public void Initialize(ArrowState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(ArrowState _newState)
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
