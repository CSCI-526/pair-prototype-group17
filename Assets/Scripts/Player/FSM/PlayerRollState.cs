using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    public PlayerRollState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        input.isRollBuffered = false;
        stateTimer = player.rollDuration;
        player.invincibleTimer = player.rollInvicibleTimeWindow; // change once using animation event
        player.invincibleTimer = player.rollDuration;            // change once using animation event
    }

    public override void Exit()
    {
        player.rollTimer = player.rollCooldown;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        // game logic
        rb.velocity = new Vector2(player.rollSpeed * player.rollDirection, rb.velocity.y);

        // state transition logic
        // can remove the below code when animation used, use AnimationOverEvent to transfer to moveState;
        if (stateTimer < 0)
        {
            AnimationOverEvent();
        }
        
    }

    public override void AnimationOverEvent()
    {
        stateMachine.ChangeState(player.moveState);
    }
}
