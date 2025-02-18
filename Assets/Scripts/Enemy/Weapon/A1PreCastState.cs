using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A1PreCastState : WeaponState
{
    bool pauseTimerStarted;
    float pauseTime = 0.1f;
    
    public A1PreCastState(Weapon _weapon, WeaponStateMachine _stateMachine) : base(_weapon, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        weapon.curveDoneSwitch = false;
        pauseTimerStarted = false;
        weapon.a1PreCastCurve.StartMove();

    }

    public override void Exit()
    {
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
            weapon.enemy.enemyEye.color = Color.red;


        }
        if (stateTimer < 0 && pauseTimerStarted)
        {
            stateMachine.ChangeState(weapon.a1State);
        }
        
        
        //Debug.Log("Weapon Attack");
    }



}

