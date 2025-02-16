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


        weapon.TrackBackOriginator();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
    }
    public override void Update()
    {
        rb.velocity = weapon.transform.up * weapon.moveSpeed;
        rb.angularVelocity = 0;
        base.Update();
    }
}

