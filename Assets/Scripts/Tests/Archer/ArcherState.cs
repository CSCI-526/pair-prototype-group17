using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherState
{
    // Start is called before the first frame update
    protected ArcherStateMachine stateMachine;
    protected Archer archer;
    protected Rigidbody2D rb;
    protected float stateTimer;

    public ArcherState(Archer _archer, ArcherStateMachine _stateMachine)
    {
        this.archer = _archer;
        this.stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {

        rb = archer.rb;
    }

    public virtual void Exit()
    {

    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;


    }

   
}
