using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;
    protected Rigidbody2D rb;
    public string animBoolName;
    protected float stateTimer;

    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName)
    {
        this.enemy = _enemy;
        this.stateMachine = _stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        rb = enemy.rb;
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void Update()
    {
        
        stateTimer -= Time.deltaTime;
        if (enemy.health <= 0)
        {
            stateMachine.ChangeState(enemy.deathState);
            return;
        }

    }

    public virtual void LateUpdate()
    {

    }

    public virtual void AnimationOverEvent()
    {

    }

}
