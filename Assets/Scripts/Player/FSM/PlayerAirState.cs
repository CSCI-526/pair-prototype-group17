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

        if (Input.GetKeyDown(KeyCode.J) && player.attackTimer < 0)
        {
            stateMachine.ChangeState(player.attackState);
            return;
        }
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.moveState);
            return;
        }

        if (xInput != 0)
        {
            rb.velocity = new Vector2(player.moveSpeed * xInput, rb.velocity.y);
        }

        if (player.IsWallDetected() && player.wallSlideTimer < 0)
        {
            stateMachine.ChangeState(player.wallSlideState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && player.jumpCounter < player.maxJumpsAllowed)
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.L) && player.dashCounter < player.maxDashesAllowed)
        {
            player.dashDirection = Input.GetAxisRaw("Horizontal");

            if (player.dashDirection == 0)
            {
                player.dashDirection = player.facingDir;
            }
            stateMachine.ChangeState(player.dashState);
            return;
        }
    }
}
