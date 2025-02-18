using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowState : MonoBehaviour
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
}
