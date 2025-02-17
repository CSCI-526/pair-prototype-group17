using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;
    protected PlayerInput input;
    public string animBoolName;
    protected float stateTimer;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        input = player.input;
        //Debug.Log("I enter " + animBoolName);
    }

    public virtual void Exit() {
        player.anim.SetBool(animBoolName, false);
        stateTimer = 0;
        //Debug.Log("I exit " + animBoolName);
    }

    public virtual void Update() {
        player.anim.SetFloat("yVelocity", rb.velocity.y);
        player.FlipController(input.Xinput);
        stateTimer -= Time.deltaTime;
        if (player.isDamaged)
        {
            stateMachine.ChangeState(player.onDamageState);
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
