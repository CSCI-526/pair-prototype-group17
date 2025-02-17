using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnDamageState : EnemyState
{
    public EnemyOnDamageState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName) : base(_enemy, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        stateTimer = 0.2f; // pause time when hit
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if (stateTimer < 0) {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
