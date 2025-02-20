using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Arrow;

public class ArrowTrackingState : ArrowState
{
    // Start is called before the first frame update
    public ArrowTrackingState(Arrow _arrow, ArrowStateMachine _stateMachine) : base(_arrow, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //rb.bodyType = RigidbodyType2D.Dynamic;
        arrow.canDoDamage = true;
        arrow.SetColor(Color.red);

    }

    public override void Exit()
    {
        arrow.canDoDamage = false;
        
        
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (arrow.mode == Arrow.Mode.Track)
        {
            arrow.TrackTarget();
        }
        else
        {
            rb.velocity = arrow.transform.right * arrow.moveSpeed;
            rb.angularVelocity = 0;
        }
        
        
        //if (arrow.isParried)
        //{
        //    stateMachine.ChangeState(arrow.trackBackState);
        //    return;
        //}
    }

    public override void OnParry()
    {
        arrow.HitPauseAndCameraShake();
        stateMachine.ChangeState(arrow.trackBackState);
        return;
    }
    public override void OnJumpParry()
    {
        arrow.HitPauseAndCameraShake();
        stateMachine.ChangeState(arrow.disFunctionState);
        return;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        switch (arrow.tutorial)
        {
            case TutorialType.None:
                break;
            case TutorialType.Attack:
                if (other.CompareTag("PlayerAtkBox"))
                {
                    TimeManager.instance.PauseUntilJPressed();
                }
                break;
            case TutorialType.Jump:
                if (other.CompareTag("PlayerJmpBox"))
                {
                    TimeManager.instance.PauseUntilSpacePressed();
                }
                break;
        }
    }
}
