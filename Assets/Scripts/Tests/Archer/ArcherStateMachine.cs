using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherStateMachine
{
    // Start is called before the first frame update
    public ArcherState currentState { get; private set; }
    public ArcherState previousState { get; private set; }

    public void Initialize(ArcherState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(ArcherState _newState)
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
