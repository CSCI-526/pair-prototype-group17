using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEnemy : Entity
{
    #region States
    public EntityState idleState { get; private set; }
    public EntityState moveState { get; private set; }  
    #endregion
    public override void Awake()
    {
        base.Awake();
        idleState = new EntityIdleState(this, stateMachine, "Idle");
        moveState = new EntityMoveState(this, stateMachine, "Move");
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
    public override void checkFacingDir()
    {
        base.checkFacingDir();
    }

    public override void Flip()
    {
        base.Flip();
    }

    public override void FlipController(float _x)
    {
        base.FlipController(_x);
    }

    public override bool IsGroundDetected()
    {
        return base.IsGroundDetected();
    }

    public override bool IsWallDetected()
    {
        return base.IsWallDetected();
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    
}
