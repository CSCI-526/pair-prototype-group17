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
        player.jumpBoxIndicator.SetActive(true);
        stateTimer = player.jumpParryWindow;
        player.jumpCounter++;
        player.dashCounter = 0;
        player.invincibleTimer = player.jumpParryWindow;
        input.isJumpBuffered = false;
        rb.velocity = new Vector2(rb.velocity.x, player.jumpSpeed);
    }

    public override void Exit()
    {
        player.jumpBoxIndicator.SetActive(false);
        player.invincibleTimer = 0;
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
        // game logic
        // jump parry
        if (stateTimer > 0)
        {
            Vector2 jumpBoxCenter = (Vector2)player.transform.position + player.jumpBoxCenterOffset;
            Vector2 jumpBoxTopLeftCorner = jumpBoxCenter - new Vector2(player.jumpBoxWidth / 2, player.jumpBoxHeight / 2);
            Vector2 jumpBoxBottomRightCorner = jumpBoxCenter + new Vector2(player.jumpBoxWidth / 2, player.jumpBoxHeight / 2);
            Collider2D[] colliders = Physics2D.OverlapAreaAll(jumpBoxTopLeftCorner, jumpBoxBottomRightCorner,player.canBeJumpParried);
            bool isParried = false;
            foreach (var hit in colliders)
            {
                HomingMissile missile = hit.GetComponent<HomingMissile>();
                if (missile!= null)
                {
                    missile.DisableMovement();
                    isParried = true;
                }
            }
            if (isParried)
            {
                player.invincibleTimer = player.jumpParryInivicibleTimeWindow;
                rb.velocity = new Vector2(rb.velocity.x, player.jumpSpeed * 1.2f);
                player.jumpCounter = 0;
            }
        }
        else 
        { 
            player.jumpBoxIndicator.SetActive(false); 
        }

        // state transition logic
        if ((input.Attack || input.isAttackBuffered) && player.attackTimer < 0)
        {
            stateMachine.ChangeState(player.attackState);
            return;
        }
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
            return;
        }
        if (input.Xinput != 0)
        {
            rb.velocity = new Vector2(player.moveSpeed * input.Xinput * player.jumpXSpeedMultiplier, rb.velocity.y);
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
