using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArcherDeathState : ArcherState
{
    public ArcherDeathState(Archer _archer, ArcherStateMachine _stateMachine) : base(_archer, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = Vector3.zero;
        rb.velocity = new Vector2(-archer.facingDir * 2, 2);
        archer.DestroyMe(4);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        
        base.Update();
        //Debug.Log("DDDDDD");
    }
}
