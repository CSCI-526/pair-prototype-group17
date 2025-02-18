using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WeaponBrokenState : WeaponState
{
    public WeaponBrokenState(Weapon _weapon, WeaponStateMachine _stateMachine) : base(_weapon, _stateMachine)
    {
    }
    public override void Enter()
    {
        
        base.Enter();
        weapon.SetColor(Color.gray);
        weapon.enemy.isVulnerable = true;
        //weapon.transform.SetParent(null, false);
        rb.velocity = new Vector2(rb.velocity.x * 0.2f, 0);
        rb.gravityScale = 3;
        stateTimer = 3; // destory after 3 seconds
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
    }
}
