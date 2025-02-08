using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    public PlayerRollState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.rollDuration;
    }

    public override void Exit()
    {
        player.rollTimer = player.rollCooldown;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(player.rollSpeed * player.rollDirection, rb.velocity.y);

        // can remove the below code when animation used, use AnimationOverEvent to transfer to moveState;
        stateTimer -= Time.deltaTime;
        if (stateTimer < 0)
        {
            AnimationOverEvent();
        }
        
    }

    public override void AnimationOverEvent()
    {
        stateMachine.ChangeState(player.moveState);
    }
}
