using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName) : base(_enemy, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //enemy.anim.SetBool("isMoving", false);
        stateTimer = 1f;
    }
    public override void Exit()
    {
        base.Exit();
        //enemy.anim.SetBool("isMoving", true);
    }
    public override void Update()
    {
        base.Update();
        //enemy.Move();
        Debug.Log("Idle");
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
