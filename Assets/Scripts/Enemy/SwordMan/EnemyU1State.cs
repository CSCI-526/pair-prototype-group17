using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyU1State : EnemyState
{
    public EnemyU1State(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName) : base(_enemy, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //enemy.weaponController.transform.rotation = Quaternion.Euler(0, 0, 90);
        enemy.weaponController.StartTracking();
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
