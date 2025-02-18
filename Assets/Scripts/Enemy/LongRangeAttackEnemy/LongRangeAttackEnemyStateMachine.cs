using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAttackEnemyStateMachine
{
    public LongRangeAttackEnemyState currentState { get; private set; }
    public LongRangeAttackEnemyState previousState { get; private set; }

    public void Initialize(LongRangeAttackEnemyState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(LongRangeAttackEnemyState _newState)
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
