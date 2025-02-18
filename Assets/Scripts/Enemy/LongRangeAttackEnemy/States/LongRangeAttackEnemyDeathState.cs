using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAttackEnemyDeathState : LongRangeAttackEnemyState
{
    public LongRangeAttackEnemyDeathState(LongRangeAttackEnemy _longRangeAttackEnemy, LongRangeAttackEnemyStateMachine _stateMachine, string animBoolName) : base(_longRangeAttackEnemy, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        longRangeAttackEnemy.rb.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        longRangeAttackEnemy.rb.velocity = Vector3.zero;
        longRangeAttackEnemy.rb.gravityScale = 3;
        longRangeAttackEnemy.ForceMove(-2f);
        longRangeAttackEnemy.ForceJump(1f);
        stateTimer = 4f;
        longRangeAttackEnemy.StartEyeColorChange(new Color(123 / 255f, 25 / 255f, 40 / 255f), Color.gray, 1f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer < 0)
        {
            longRangeAttackEnemy.DestroyMe();
        }
    }
}
