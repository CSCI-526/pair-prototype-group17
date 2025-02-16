using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WeaponIdleState : WeaponState
{
    public WeaponIdleState(Weapon _weapon, WeaponStateMachine _stateMachine) : base(_weapon, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        weapon.transform.position = weapon.InitialPostion * weapon.enemy.facingDir + weapon.enemy.transform.position;
        weapon.transform.rotation = Quaternion.Euler(weapon.InitialRotation);// initial position
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
        base.Update();
        //Debug.Log("Weapon Idle");
        
        if (weapon.a1Switch)
        {
            stateMachine.ChangeState(weapon.a1PreCastState);
        }
    }
}
