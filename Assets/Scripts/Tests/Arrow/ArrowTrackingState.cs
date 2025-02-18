using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        CameraShakeManager.instance.CameraShake(arrow.impulseSource);
        TimeManager.instance.SlowTime(0.07f, 0.1f);
        stateMachine.ChangeState(arrow.trackBackState);
        return;
    }
    public override void OnJumpParry()
    {
        CameraShakeManager.instance.CameraShake(arrow.impulseSource);
        TimeManager.instance.SlowTime(0.07f, 0.1f);
        stateMachine.ChangeState(arrow.disFunctionState);
        return;
    }

}
