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
        Debug.Log("Move");
        //enemy.Move();
        float speedAdjusted = enemy.moveSpeed;
        int playerDirection = enemy.player.transform.position.x > enemy.transform.position.x ? 1 : -1;

        if (PlayerInAttackRange())
        {
            //Debug.Log("can Attack");
            if (playerDirection != enemy.facingDir)
            {
                enemy.Flip();
            }
            if (enemy.weaponController.hitCounter >= 2)
            {
                stateMachine.ChangeState(enemy.u1State);
                return;
            }
            stateMachine.ChangeState(enemy.attackState);
            return;
        }
        if (PlayerInRange())
        {
            if (playerDirection != enemy.facingDir)
            {
                enemy.Flip();
            }
            rb.velocity = new Vector2(enemy.moveSpeed*1.5f * enemy.facingDir, rb.velocity.y);
            return;
        }
        
        else if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
        rb.velocity = new Vector2(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);
    }

    private bool PlayerInRange()
    {
        float distX = Mathf.Abs(enemy.player.transform.position.x - enemy.transform.position.x);
        float distY = Mathf.Abs(enemy.player.transform.position.y - enemy.transform.position.y);

        return distX <= enemy.playerDetectionRangeX/2 && distY <= enemy.playerDetectionRangeY/2;
    }

    private bool PlayerInAttackRange()
    {
        float distX = Mathf.Abs(enemy.player.transform.position.x - enemy.transform.position.x);
        float distY = Mathf.Abs(enemy.player.transform.position.y - enemy.transform.position.y);

        return distX <= enemy.attackRangeX / 2 && distY <= enemy.attackRangeY / 2;
    }

}   

