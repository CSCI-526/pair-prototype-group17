using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAttackEnemyState 
{
    protected LongRangeAttackEnemyStateMachine stateMachine;
    protected LongRangeAttackEnemy longRangeAttackEnemy;
    protected Rigidbody2D rb;
    public string animBoolName;
    protected float stateTimer;

    public LongRangeAttackEnemyState(LongRangeAttackEnemy _longRangeAttackEnemy,LongRangeAttackEnemyStateMachine _stateMachine, string animBoolName)
    {
        this.longRangeAttackEnemy = _longRangeAttackEnemy;
        this.stateMachine = _stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        Debug.Log($"Enter {animBoolName}");
        rb = longRangeAttackEnemy.rb;

    }

    public virtual void Exit()
    {
        Debug.Log($"Exit {animBoolName}");
    }

    public virtual void Update()
    {

        stateTimer -= Time.deltaTime;
        if (longRangeAttackEnemy.health <= 0)
        {
            stateMachine.ChangeState(longRangeAttackEnemy.deathState);
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
