using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using static WeaponController;

public class WeaponTrackBackState : WeaponState
{
    public WeaponTrackBackState(Weapon _weapon, WeaponStateMachine _stateMachine) : base(_weapon, _stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        weapon.canDoDamageToOwner = true;
        

        weapon.TrackBackOriginator();
        stateTimer = 7f;
    }
    public override void Exit()
    {
        weapon.canDoDamageToOwner = false;
        base.Exit();
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
    }
    public override void Update()
    {
        base.Update();
        rb.velocity = weapon.transform.up * weapon.moveSpeed;
        rb.angularVelocity = 0;
        if (weapon.isStackedOnOwner)
        {
            stateMachine.ChangeState(weapon.stackOnOwnerState);
            return;
        }
        if (stateTimer < 0)
        {
            weapon.ResetToIdle();
            stateMachine.ChangeState(weapon.idleState);
            weapon.enemy.stateMachine.ChangeState(weapon.enemy.idleState);
        }

    }
}

