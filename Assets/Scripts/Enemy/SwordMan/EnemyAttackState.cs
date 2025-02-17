using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        enemy.attackOver = false;
        enemy.rb.velocity = Vector2.zero;
        enemy.weapon.StartAttack1();
        

    }
    public override void Exit()
    {
       
        base.Exit();
        
        
    }
    public override void Update()
    {
        base.Update();
       

        if (enemy.attackOver)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }
        

        //Debug.Log("Attack");
    }
}

