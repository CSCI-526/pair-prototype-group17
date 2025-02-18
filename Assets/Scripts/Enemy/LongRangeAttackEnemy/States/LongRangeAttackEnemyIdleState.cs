using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAttackEnemyIdleState : LongRangeAttackEnemyState
{
    public LongRangeAttackEnemyIdleState(LongRangeAttackEnemy _longRangeAttackEnemy, LongRangeAttackEnemyStateMachine _stateMachine, string _animBoolName) : base(_longRangeAttackEnemy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        Debug.Log("Idle");
        if (DoesPlayerReachTheAttackArea())
        {
            Debug.Log("Change current state to attackState.");
            stateMachine.ChangeState(longRangeAttackEnemy.attackState);
        }
        //rb.velocity = new Vector2(0, rb.velocity.y);
        //if (stateTimer < 0)
        //{
        //    stateMachine.ChangeState(longRangeAttackEnemy.moveState);
        //    return;
        //}
    }

    public bool DoesPlayerReachTheAttackArea()
    {
        float dis = Vector2.Distance(longRangeAttackEnemy.target.transform.position, longRangeAttackEnemy.transform.position);
        if (dis <= longRangeAttackEnemy.detectionCircleRadius)
        {
            Debug.Log("Detect the player.");
            return true;
        }
        return false;
    }
}
