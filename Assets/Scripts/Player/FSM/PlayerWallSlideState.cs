using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.wallSlideMinDuration;
    }

    public override void Exit()
    {
        player.wallSlideTimer = player.wallSlideCoolDown;
        player.jumpCounter = 0;
        player.dashCounter = 0;
        stateTimer = 0;
        base.Exit();
        
    }

    public override void Update()
    {
        // game logic
        stateTimer -= Time.deltaTime;
        rb.velocity = new Vector2(0, -player.wallSlideSpeed);

        // state transition logic
        if ((input.Attack || input.isAttackBuffered) && player.attackTimer < 0)
        {
            player.Flip();
            stateMachine.ChangeState(player.attackState);
            return;
        }

        if (stateTimer < 0){
            
            if (input.Jump || input.isJumpBuffered)
            {
                stateMachine.ChangeState(player.wallJumpState);
                return;
            }

            if ((input.Xinput != 0 && player.facingDir * input.Xinput < 0) || !player.IsWallDetected())
            {
                stateMachine.ChangeState(player.airState);
                return;
            }

            if (player.IsGroundDetected())
            {
                stateMachine.ChangeState(player.idleState);
                return;
            }
        }
        
    }
}
