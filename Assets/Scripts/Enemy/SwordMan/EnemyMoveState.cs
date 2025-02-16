using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName) : base(_enemy, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //enemy.anim.SetBool("isMoving", true);
    }
    public override void Exit()
    {
        base.Exit();
        //enemy.anim.SetBool("isMoving", false);
    }
    public override void Update()
    {
        base.Update();
        //Debug.Log("Move");
        //enemy.Move();
        float speedAdjusted = enemy.moveSpeed;
        int playerDirection = enemy.player.transform.position.x > enemy.transform.position.x ? 1 : -1;

        var proximity = enemy.PlayerInRange();
        if (proximity.inAttackRange)
        {
            //Debug.Log("can Attack");
            if (playerDirection != enemy.facingDir)
            {
                enemy.Flip();
            }
            //if (enemy.weaponController.hitCounter >= 2)
            //{
            //    stateMachine.ChangeState(enemy.u1State);
            //    return;
            //}
            stateMachine.ChangeState(enemy.attackState);
            return;
        }
        if (proximity.inRange)
        {
            if (playerDirection != enemy.facingDir)
            {
                enemy.Flip();
            }
            rb.velocity = new Vector2(enemy.moveSpeed * 1.5f * enemy.facingDir, rb.velocity.y);
            return;
        }

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
        rb.velocity = new Vector2(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);
    }


}   

