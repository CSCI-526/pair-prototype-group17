using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        player.jumpCounter = 0;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // state transition logic
        if ((input.Attack||input.isAttackBuffered) && player.attackTimer < 0)
        {
            stateMachine.ChangeState(player.attackState);
            return;
        }
        if ((input.Jump || input.isJumpBuffered) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        if ((input.Roll|| input.isRollBuffered) && player.IsGroundDetected() && player.rollTimer < 0)
        {
            player.rollDirection = input.Xinput;

            // get most recent input direction when rolling
            if (player.rollDirection == 0)
            {
                player.rollDirection = player.facingDir;
            }
            stateMachine.ChangeState(player.rollState);
            return;
        }

        if (!player.IsGroundDetected()) {
            stateMachine.ChangeState(player.airState);
            return;
        }
        
    }
}
