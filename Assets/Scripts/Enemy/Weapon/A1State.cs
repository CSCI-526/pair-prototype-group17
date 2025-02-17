using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A1State : WeaponState
{
    bool pauseTimerStarted;
    float pauseTime = 0.2f;
    public A1State(Weapon _weapon, WeaponStateMachine _stateMachine) : base(_weapon, _stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        weapon.curveDoneSwitch = false;
        pauseTimerStarted = false;
        weapon.canDoDamage = true;
        weapon.canBeParried = true;
        weapon.enemy.ForceMove(2f);
        weapon.a1Curve.StartMove();
        
    }
    public override void Exit()
    {
        
        
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (weapon.curveDoneSwitch)
        {
            weapon.curveDoneSwitch = false;
            stateTimer = pauseTime;
            pauseTimerStarted = true;
            weapon.canDoDamage = false;
            weapon.canBeParried = false;


        }
        //List<Collider2D> colliders = new List<Collider2D>();
        //ContactFilter2D contactFilter = new ContactFilter2D();
        //contactFilter.SetLayerMask(weapon.attackDetectionLayer);

        //int count = Physics2D.OverlapCollider(weapon.cldr, contactFilter, colliders);
        //foreach (var hit in colliders)
        //{
        //    Player player = hit.GetComponent<Player>();
        //    if (player != null)
        //    {

        //        //weapon.canDoDamage = false;
        //        weapon.OnParry();
        //        player.OnHit(weapon.enemy.transform, 4, true);
        //        isParried = true;



        //    }
        //}
        if (stateTimer < 0 && pauseTimerStarted)
        {
            stateMachine.ChangeState(weapon.a1PostCastState);
        }
        //Debug.Log("Weapon Attack");
    }
}
