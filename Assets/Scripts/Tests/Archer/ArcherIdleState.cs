using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : ArcherState
{
    public ArcherIdleState(Archer _archer, ArcherStateMachine _stateMachine) : base(_archer, _stateMachine)
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
        
            if (archer.PlayerInRange().inRange && archer.canAttack)
            {
            stateMachine.ChangeState(archer.shootState);

            }
            

        
        
    }
}
