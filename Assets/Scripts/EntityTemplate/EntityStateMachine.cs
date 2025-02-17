using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateMachine
{
    // Start is called before the first frame update
    public EntityState currentState { get; private set; }
    public EntityState previousState { get; private set; }

    public virtual void Initialize(EntityState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public virtual void ChangeState(EntityState _newState)
    {
        previousState = currentState;
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }

    public virtual void ChangeToPreviousState()
    {
        currentState.Exit();
        currentState = previousState;
        currentState.Enter();
    }
}
