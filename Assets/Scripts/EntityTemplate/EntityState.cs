using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState : MonoBehaviour
{
    protected EntityStateMachine stateMachine;
    protected Entity entity;
    protected Rigidbody2D rb;
    public string animBoolName;
    protected float stateTimer;

    public EntityState(Entity _entity, EntityStateMachine _stateMachine, string animBoolName)
    {
        this.entity = _entity;
        this.stateMachine = _stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        rb = entity.rb;

    }

    public virtual void Exit()
    {

    }

    public virtual void Update()
    {

        stateTimer -= Time.deltaTime;

    }

    public virtual void LateUpdate()
    {

    }



}
