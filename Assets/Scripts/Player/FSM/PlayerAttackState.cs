using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationOverEvent()
    {
        if (stateMachine.previousState is PlayerGroundedState)
        {
            stateMachine.ChangeToPreviousState();
        }
        else
        {
            stateMachine.ChangeState(player.airState);
        }
        
    }

    public override void Enter()
    {
        base.Enter();
        player.attackIndicator.SetActive(true);
        player.attackTimer = player.attackCoolDown;
        player.animWeapon.SetBool("BigSlash", true);
        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + player.attackJumpForce/4);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, player.attackJumpForce);
        }
        stateTimer = player.attackDuration;
    }

    public override void Exit()
    {
        player.attackIndicator.SetActive(false);
        player.animWeapon.SetBool("BigSlash", false);
        rb.velocity = new Vector2(rb.velocity.x, 0);
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //rb.velocity = new Vector2(4*player.facingDir, rb.velocity.y * 0.6f);
        stateTimer -= Time.deltaTime;
        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        if (stateTimer < 0)
        {
            AnimationOverEvent();
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        //rb.velocity = new Vector2(rb.velocity.x*0.6f, rb.velocity.y * 0.6f);
    }
}
