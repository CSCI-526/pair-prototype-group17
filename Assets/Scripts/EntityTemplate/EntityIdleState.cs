using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is just an example of a state, so that we have something in the idle state for the template
public class EntityIdleState : EntityState
{
    // Start is called before the first frame update
    public EntityIdleState(Entity _entity, EntityStateMachine _stateMachine, string animBoolName) : base(_entity, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("I Enter Idle");
    }

    public override void Exit()
    {
        Debug.Log("I Exit Idle");
        base.Exit();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (stateTimer < 0)
        {
            //stateMachine.ChangeState(entity.moveState);
            return; // remember to return right after state change if you don't wish to do codes after this when you change state
        }
        Debug.Log("I am in Idle");
    }
}
