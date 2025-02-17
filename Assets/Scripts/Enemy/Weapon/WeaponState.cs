using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponState
{
    // Start is called before the first frame update
    protected WeaponStateMachine stateMachine;
    protected Weapon weapon;
    protected Rigidbody2D rb;
    protected float stateTimer;

    public WeaponState(Weapon _weapon, WeaponStateMachine _stateMachine)
    {
        this.weapon = _weapon;
        this.stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        
        rb = weapon.rb;
    }

    public virtual void Exit()
    {
        
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        switch (weapon.hitCounter)
        {
            case 0:
                weapon.SetColor(Color.white);
                break;
            case 1:
                weapon.SetColor(Color.yellow);
                break;
            case 2:
                weapon.SetColor(Color.red);
                break;

        }

    }

    public virtual void LateUpdate()
    {

    }

    public virtual void OnParry()
    {
        if (weapon.canBeParried)
        {
            CameraShakeManager.instance.CameraShake(weapon.impulseSource);
            TimeManager.instance.SlowTime(0.07f, 0.1f);
            if (weapon.hitCounter < 2)
            {
                weapon.hitCounter++;
            }
            weapon.canDoDamage = false;
            weapon.canBeParried = false;
        }
    }
    public virtual void OnJumpParry()
    {
        if (weapon.canBeParried)
        {
            CameraShakeManager.instance.CameraShake(weapon.impulseSource);
            TimeManager.instance.SlowTime(0.07f, 0.1f);
            weapon.canDoDamage = false;
            weapon.canBeParried = false;
        }
    }
}
