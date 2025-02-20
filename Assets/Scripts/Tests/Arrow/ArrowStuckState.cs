using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowStuckState : ArrowState
{
    public ArrowStuckState(Arrow _arrow, ArrowStateMachine _stateMachine) : base(_arrow, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        arrow.DestroyParentEntity();
        arrow.DestroyMe(3);
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
