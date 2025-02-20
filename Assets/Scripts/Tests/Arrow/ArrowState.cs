using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowState
{
    // Start is called before the first frame update
    protected ArrowStateMachine stateMachine;
    protected Arrow arrow;
    protected Enemy enemy;
    protected Rigidbody2D rb;
    protected float stateTimer;

    public ArrowState(Arrow _arrow, ArrowStateMachine _stateMachine)
    {
        this.arrow = _arrow;
        this.stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {

        rb = arrow.rb;
    }

    public virtual void Exit()
    {

    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        

    }

    public virtual void OnParry()
    {

    }
    public virtual void OnJumpParry()
    {

    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {

    }
}
