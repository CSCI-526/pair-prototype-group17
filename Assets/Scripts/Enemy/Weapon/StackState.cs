using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackState : WeaponState
{
    // Start is called before the first frame update
    public StackState(Weapon _weapon, WeaponStateMachine _stateMachine) : base(_weapon, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
        weapon.enemy.stateMachine.ChangeState(weapon.enemy.deathState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
