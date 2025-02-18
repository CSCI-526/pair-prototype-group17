using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WeaponTrackState : WeaponState
{
    bool trigger;
    public WeaponTrackState(Weapon _weapon, WeaponStateMachine _stateMachine) : base(_weapon, _stateMachine)
    {
    }
    public override void Enter()
    {
        
        base.Enter();
        weapon.rb.bodyType = RigidbodyType2D.Dynamic;
        trigger = true;
        weapon.canDoDamage = true;
        weapon.canBeParried = true;
        weapon.transform.up = Vector2.right * weapon.enemy.facingDir;
        weapon.enemy.ForceMove(-2f);
        stateTimer = 4f;
        //stateTimer = 1f;
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
        weapon.TrackTarget();
        if (stateTimer < 0)
        {
            weapon.ResetToIdle();
            stateMachine.ChangeState(weapon.idleState);
            weapon.enemy.stateMachine.ChangeState(weapon.enemy.idleState);
        }
        //if (stateTimer < 0 && trigger)
        //{
        //    weapon.enemy.ForceJump(4f);
        //    trigger = false;


        //}
    }
    public override void OnParry()
    {
        if (weapon.canBeParried)
        {
            CameraShakeManager.instance.CameraShake(weapon.impulseSource);
            TimeManager.instance.SlowTime(0.07f, 0.1f);
            stateMachine.ChangeState(weapon.trackBackState);
            weapon.canDoDamage = false;
            weapon.canBeParried = false;
        }
            
    }

    public override void OnJumpParry()
    {
        base.OnJumpParry();
        stateMachine.ChangeState(weapon.brokenState);

    }


}
