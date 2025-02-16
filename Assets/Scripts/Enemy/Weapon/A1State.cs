using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A1State : WeaponState
{
    bool pauseTimerStarted;
    float pauseTime = 0.2f;
    public A1State(Weapon _weapon, WeaponStateMachine _stateMachine) : base(_weapon, _stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        weapon.curveDoneSwitch = false;
        pauseTimerStarted = false;
        weapon.canDoDamage = true;
        weapon.enemy.ForceMove(2f);
        weapon.a1Curve.StartMove();
        
    }
    public override void Exit()
    {
        weapon.canDoDamage = false;
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (weapon.curveDoneSwitch)
        {
            weapon.curveDoneSwitch = false;
            stateTimer = pauseTime;
            pauseTimerStarted = true;


        }
        if (stateTimer < 0 && pauseTimerStarted)
        {
            stateMachine.ChangeState(weapon.a1PostCastState);
        }
        //Debug.Log("Weapon Attack");
    }
}
