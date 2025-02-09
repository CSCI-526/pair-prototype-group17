using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationOverEvent()
    {
        stateMachine.ChangeState(player.airState);
    }

    public override void Enter()
    {
        base.Enter();
        player.dashCounter++;
        stateTimer = player.dashDuration;
        player.invincibleTimer = player.dashInvicibleTimeWindow; // change once using animation event
        player.invincibleTimer = player.dashDuration;            // change once using animation event
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(player.dashSpeed * player.dashDirection, 0);
        stateTimer -= Time.deltaTime;// remove once add animation
        if (player.IsWallDetected() && !player.IsGroundDetected() && player.wallSlideTimer < 0)
        {
            stateMachine.ChangeState(player.wallSlideState);
            return;
        }

        // remove above once add animation
        if (stateTimer < 0)
        {
            AnimationOverEvent();
        }
    }
}
