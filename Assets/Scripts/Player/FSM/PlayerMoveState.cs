using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        
        // game logic
        float targetSpeed = input.Xinput * player.moveSpeed;
        float speedDifference = targetSpeed - rb.velocity.x;
        float frameSpeed;
        if (input.Xinput > 0)
        {
           frameSpeed = rb.velocity.x + player.acceleration * Time.deltaTime;
           rb.velocity = new Vector2(Mathf.Clamp(frameSpeed, -player.moveSpeed, player.moveSpeed), rb.velocity.y);
        }
        else if (input.Xinput < 0) 
        {
        
            frameSpeed = rb.velocity.x - player.acceleration * Time.deltaTime;
            rb.velocity = new Vector2(Mathf.Clamp(frameSpeed, -player.moveSpeed, player.moveSpeed), rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -player.moveSpeed, player.moveSpeed), rb.velocity.y);
        }

        // animation logic
        MoveStateCustomAnimationTransition();

        // state transition logic
        if (Mathf.Abs(input.Xinput) == 0 && Mathf.Abs(rb.velocity.x) < player.stopSpeed)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Stop completely if almost stopping
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void MoveStateCustomAnimationTransition()
    {
        bool xInputCheck = input.Xinput != 0;
        bool xspeedCheck = Mathf.Abs(rb.velocity.x) > 7.9f;


        if (xInputCheck)
        {
            if (player.IsWallDetected())
            {
                player.ChangeAnimationState("player_run");
            }
            else
            {
                if (xspeedCheck)
                    player.ChangeAnimationState("player_run");
                else
                    player.ChangeAnimationState("player_idle_to_run");
            }
            
        }
        else
        {
            if (xspeedCheck)
                player.ChangeAnimationState("player_run");
            else
                player.ChangeAnimationState("player_run_to_idle");
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        
    }
}
