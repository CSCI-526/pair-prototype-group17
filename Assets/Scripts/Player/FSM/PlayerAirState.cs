using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        // game logic
        if (input.Xinput != 0)
        {
            rb.velocity = new Vector2(player.moveSpeed * input.Xinput, rb.velocity.y);
        }

        // state transition logic
        if ((input.Attack || input.isAttackBuffered) && player.attackTimer < 0)
        {
            stateMachine.ChangeState(player.attackState);
            return;
        }
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.moveState);
            return;
        }
        if (player.IsWallDetected() && player.wallSlideTimer < 0)
        {
            stateMachine.ChangeState(player.wallSlideState);
            return;
        }
        if ((input.Jump || input.isJumpBuffered) && player.jumpCounter < player.maxJumpsAllowed)
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }
        if ((input.Roll || input.isRollBuffered) && player.dashCounter < player.maxDashesAllowed)
        {
            // get most recent input direction when dashing
            player.dashDirection = input.Xinput;

            if (player.dashDirection == 0)
            {
                player.dashDirection = player.facingDir;
            }
            stateMachine.ChangeState(player.dashState);
            return;
        }
    }
}
