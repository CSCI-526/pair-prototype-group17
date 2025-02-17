using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnDamageState : PlayerState
{
    public PlayerOnDamageState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.health -= 10;
        CameraShakeManager.instance.CameraShake(player.impulseSource);
        stateTimer = player.OnDamageCoolDown;
        if (stateMachine.previousState != player.wallSlideState)
        {
            if (player.knockBackForce.y == 0)
            {
                rb.velocity = new Vector2(player.knockBackForce.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(player.knockBackForce.x, player.knockBackForce.y);
            }
        }
        
        //rb.velocity = ()
    }

    public override void Exit()
    {
        player.isDamaged = false;
        base.Exit();
    }

    public override void Update()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer < 0)
        {
            if (player.IsGroundDetected()) {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.airState);
            }
            
        }
        //player.playerPrototypeSprite.color = Color.red;
    }
}
