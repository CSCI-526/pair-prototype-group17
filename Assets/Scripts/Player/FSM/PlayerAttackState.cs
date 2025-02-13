using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
        input.isAttackBuffered = false;
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
        player.invincibleTimer = player.attackInvicibleTimeWindow; // change once using animation event
        player.invincibleTimer = player.attackDuration;            // change once using animation event
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
        AttackParryCheck();
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

    private void AttackParryCheck()
    {
        Vector2 attackBoxCenter;
        Vector2 attackBoxTopLeftCorner;
        Vector2 attackBoxBottomRightCorner;
        float boxCenterX;
        float boxCenterY;
        if (player.facingRight)
        {
            boxCenterX = player.transform.position.x + player.attackBoxCenterOffset.x;
            boxCenterY = player.transform.position.y + player.attackBoxCenterOffset.y;
              
        }
        else
        {
            boxCenterX = player.transform.position.x - player.attackBoxCenterOffset.x;
            boxCenterY = player.transform.position.y + player.attackBoxCenterOffset.y;
            
        }
        attackBoxCenter = new Vector2(boxCenterX, boxCenterY);
        attackBoxTopLeftCorner = attackBoxCenter - new Vector2(player.attackBoxWidth / 2, player.attackBoxHeight / 2);
        attackBoxBottomRightCorner = attackBoxCenter + new Vector2(player.attackBoxWidth / 2, player.attackBoxHeight / 2);
       

        Collider2D[] colliders = Physics2D.OverlapAreaAll(attackBoxTopLeftCorner, attackBoxBottomRightCorner, player.canBeAttackParried);
        bool isParried = false;
        foreach (var hit in colliders)
        {
            InteractableProjectile missile = hit.GetComponent<InteractableProjectile>();
            if (missile != null)
            {
                missile.TrackBackOriginator();
                isParried = true;
            }
        }
        if (isParried)
        {
            
            //rb.velocity = new Vector2(rb.velocity.x, player.jumpSpeed * 1.2f);
            //player.jumpCounter = 0;
        }
    }
}
