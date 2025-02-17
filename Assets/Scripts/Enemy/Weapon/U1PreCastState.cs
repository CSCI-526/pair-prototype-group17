using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U1PreCastState : WeaponState
{
    // Start is called before the first frame update
    bool pauseTimerStarted;
    float pauseTime = 0.3f;
    public U1PreCastState(Weapon _weapon, WeaponStateMachine _stateMachine) : base(_weapon, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        weapon.curveDoneSwitch = false;
        pauseTimerStarted = false;
        weapon.enemy.checkFacingDir();
        weapon.u1PreCastCurve.StartMove();
        weapon.enemy.ForceMove(-3f);
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


        }
        if (stateTimer < 0 && pauseTimerStarted)
        {
            stateMachine.ChangeState(weapon.trackState  );
        }
    }
}
