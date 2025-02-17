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
        
        base.Enter();
        stateTimer = 0.2f;// pause time when hit
        enemy.enemyPrototypeSprite.color = Color.red;
    }

    public override void Exit()
    {
        enemy.enemyPrototypeSprite.color = Color.yellow;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = Vector2.zero;
        if (stateTimer < 0) {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
