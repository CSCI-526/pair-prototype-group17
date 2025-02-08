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
        player.wallJumpFreezeTimer -= Time.deltaTime;
        
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.J) && player.wallJumpFreezeTimer < 0 && player.attackTimer < 0)
        {
            stateMachine.ChangeState(player.attackState);
            return;
        }

        if (xInput != 0 && player.wallJumpFreezeTimer < 0)
        {
            rb.velocity = new Vector2(player.moveSpeed * xInput * player.jumpXSpeedMultiplier, rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space) && player.wallJumpFreezeTimer < 0)
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.L) && player.dashCounter < player.maxDashesAllowed && player.wallJumpFreezeTimer < 0)
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
