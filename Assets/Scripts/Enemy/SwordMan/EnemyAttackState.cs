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
        //enemy.weaponController.StartLerpAtIndex(0);
        enemy.rb.velocity = Vector2.zero;
        enemy.weaponController.AttackTrailPermAOnEnter();
        
    }
    public override void Exit()
    {
        enemy.weaponController.AttackTrailPermAOnExit();
        base.Exit();
        
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
        enemy.weaponController.AttackTrailPermAOnUpdate();
        //}

        if (enemy.attackOver)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        Debug.Log("Attack");
    }
}

