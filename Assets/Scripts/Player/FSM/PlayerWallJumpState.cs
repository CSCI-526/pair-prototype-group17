using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        input.isJumpBuffered = false;
        player.jumpCounter++;
        rb.velocity = new Vector2(player.wallJumpXSpeed * -player.facingDir, player.jumpSpeed);
        player.wallJumpFreezeTimer = player.wallJumpFreezeCoolDown;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        // game logic
        player.wallJumpFreezeTimer -= Time.deltaTime;
        if (input.Xinput != 0 && player.wallJumpFreezeTimer < 0)
        {
            rb.velocity = new Vector2(player.moveSpeed * input.Xinput * player.jumpXSpeedMultiplier, rb.velocity.y);
        }

        // state transition logic
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
            return;
        }
        if ((input.Attack || input.isAttackBuffered) && player.wallJumpFreezeTimer < 0 && player.attackTimer < 0)
        {
            stateMachine.ChangeState(player.attackState);
            return;
        }
        
        if ((input.Jump || input.isJumpBuffered) && player.wallJumpFreezeTimer < 0)
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        if ((input.Roll || input.isRollBuffered) && player.dashCounter < player.maxDashesAllowed && player.wallJumpFreezeTimer < 0)
        {
            player.dashDirection = input.Xinput;
            // get most recent input direction when dashing
            if (player.dashDirection == 0)
            {
                player.dashDirection = player.facingDir;
            }
            stateMachine.ChangeState(player.dashState);
            return;
        }
    }
    
}
