using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.jumpCounter++;
        player.dashCounter = 0;
        rb.velocity = new Vector2(rb.velocity.x, player.jumpSpeed);
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
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
            return;
        }
        if (xInput != 0)
        {
            rb.velocity = new Vector2(player.moveSpeed * xInput * player.jumpXSpeedMultiplier, rb.velocity.y);
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
