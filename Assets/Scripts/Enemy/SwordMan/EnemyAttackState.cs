using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        enemy.attackOver = false;
        enemy.attackLerpCounter = enemy.maxAttackLerpCount;
        enemy.weaponController.StartLerpAtIndex(0);
        //enemy.anim.SetBool(animBoolName, true);
        enemy.rb.velocity = Vector2.zero;
        enemy.weaponController.canDoDamage = true;
    }
    public override void Exit()
    {
        base.Exit();
        enemy.weaponController.canDoDamage = false;
        //enemy.anim.SetBool(animBoolName, false);
    }
    public override void Update()
    {
        base.Update();
        //if (stateTimer <= 0)
        //{
        //    stateMachine.ChangeState(enemy.idleState);
        //}
        //if (enemy.attackOver)
        //{
        //    stateMachine.ChangeState(enemy.idleState);
        //}
        if (enemy.attackOver && enemy.attackLerpCounter == enemy.maxAttackLerpCount - 1)
        {
            enemy.weaponController.StartLerpAtIndex(1);
            enemy.attackOver = false;
        }
        if (enemy.attackOver && enemy.attackLerpCounter == enemy.maxAttackLerpCount - 2)
        {
            enemy.weaponController.StartLerpAtIndex(2);
            enemy.attackOver = false;
        }
        if (enemy.attackOver && enemy.attackLerpCounter == enemy.maxAttackLerpCount - 3)
        {
            enemy.weaponController.StartLerpAtIndex(3);
            enemy.attackOver = false;
        }
        if (enemy.attackOver && enemy.attackLerpCounter == enemy.maxAttackLerpCount - 4)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        Debug.Log("Attack");
    }
}

