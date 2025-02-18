using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherShootState : ArcherState
{
    public ArcherShootState(Archer _archer, ArcherStateMachine _stateMachine) : base(_archer, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        archer.SetAttackCoolDown();
        archer.checkFacingDir();
        if (archer.tempCounter == 0)
        {
            archer.ShootArrow(Arrow.TutorialType.Jump, Arrow.Mode.Track);
        }else if (archer.tempCounter == 1)
        {
            archer.ShootArrow(Arrow.TutorialType.Attack, Arrow.Mode.Track);
        }
        else
        {
            archer.ShootArrow(Arrow.TutorialType.None, Arrow.Mode.Track);
        }
        
    }

    public override void Exit()
    {
        archer.tempCounter++;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        stateMachine.ChangeState(archer.idleState);
    }
}
