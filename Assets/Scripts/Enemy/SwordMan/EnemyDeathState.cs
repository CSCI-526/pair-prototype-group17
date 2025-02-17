using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public EnemyDeathState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName) : base(_enemy, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.rb.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        enemy.rb.velocity = Vector3.zero;
        enemy.rb.gravityScale = 3;
        enemy.ForceMove(-2f);
        enemy.ForceJump(1f);
        stateTimer = 4f;
        enemy.StartEyeColorChange(new Color(123 / 255f, 25 / 255f, 40 / 255f), Color.gray, 1f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer < 0)
        {
            enemy.DestroyMe();
        }
    }

}
