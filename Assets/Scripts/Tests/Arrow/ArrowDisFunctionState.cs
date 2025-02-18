using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDisFunctionState : ArrowState
{
    // Start is called before the first frame update
    public ArrowDisFunctionState(Arrow _arrow, ArrowStateMachine _stateMachine) : base(_arrow, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 2;
        arrow.SetColor(Color.gray);
        stateTimer = 3; // 3 seconds before deletion
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            arrow.DestroyMe();
        }
    }
}
