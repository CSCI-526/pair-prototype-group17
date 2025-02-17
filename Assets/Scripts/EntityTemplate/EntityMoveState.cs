using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMoveState : EntityState
{
    // Start is called before the first frame update
    public EntityMoveState(Entity _entity, EntityStateMachine _stateMachine, string animBoolName) : base(_entity, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
