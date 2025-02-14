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
        //this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        //enemy.anim.SetBool(animBoolName, true);
        rb = enemy.rb;
        //Debug.Log("I enter " + animBoolName);
        //stateTimer = 0;
    }

    public virtual void Exit()
    {
        //enemy.anim.SetBool(animBoolName, false);
        
        //Debug.Log("I exit " + animBoolName);
    }

    public virtual void Update()
    {
        //player.anim.SetFloat("yVelocity", rb.velocity.y);
        //player.FlipController(rb.velocity.x);
        stateTimer -= Time.deltaTime;

    }

    public virtual void LateUpdate()
    {

    }

    public virtual void AnimationOverEvent()
    {

    }

}
