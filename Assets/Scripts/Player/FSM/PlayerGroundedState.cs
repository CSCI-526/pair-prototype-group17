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

        if (Input.GetKeyDown(KeyCode.J) && player.attackTimer < 0)
        {
            stateMachine.ChangeState(player.attackState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.L) && player.IsGroundDetected() && player.rollTimer < 0)
        {
            player.rollDirection = Input.GetAxisRaw("Horizontal");
            
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
