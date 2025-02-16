using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    // Start is called before the first frame update
    public EnemyState currentState { get; private set; }
    public EnemyState previousState { get; private set; }

    public void Initialize(EnemyState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState _newState)
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
