using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrackBackState : ArrowState
{
    public ArrowTrackBackState(Arrow _arrow, ArrowStateMachine _stateMachine) : base(_arrow, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        arrow.TrackBack();
        arrow.SetColor(Color.green);
        arrow.canDoDamage = false;
        arrow.canDoDamageToEnemy = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = arrow.transform.right * arrow.moveSpeed;
        rb.angularVelocity = 0;
        
    }

    // Start is called before the first frame update

}
