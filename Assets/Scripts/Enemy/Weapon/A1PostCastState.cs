using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A1PostCastState : WeaponState
{
    public A1PostCastState(Weapon _weapon, WeaponStateMachine _stateMachine) : base(_weapon, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        weapon.curveDoneSwitch = false;
        
        //pauseTimerStarted = false;
        weapon.a1PostCastCurve.StartMove();
    }

    public override void Exit()
    {
        

        base.Exit();
    }

    public override void Update()
    {
        if (weapon.curveDoneSwitch)
        {

            weapon.a1Switch = false;
            
            if (weapon.hitCounter >= 2)
            {
                stateMachine.ChangeState(weapon.u1PreCastState);
            }
            else
            {
                stateMachine.ChangeState(weapon.idleState);
                weapon.enemy.attackOver = true;
            }
            

        }
        base.Update();
    }
}
