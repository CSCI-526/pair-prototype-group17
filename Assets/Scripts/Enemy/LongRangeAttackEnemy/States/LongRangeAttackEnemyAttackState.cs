using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAttackEnemyAttackState : LongRangeAttackEnemyState
{
    private float time_recorder = 0.0f;
    private int missile_counter = 0;
    private int missile_counter_per_attack_round = 0;

    public LongRangeAttackEnemyAttackState(LongRangeAttackEnemy _longRangeAttackEnemy, LongRangeAttackEnemyStateMachine _stateMachine, string _animBoolName) : base(_longRangeAttackEnemy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        longRangeAttackEnemy.attackOver = false;
        longRangeAttackEnemy.rb.velocity = Vector2.zero;
        //longRangeAttackEnemy.weapon.StartAttack1();


    }
    public override void Exit()
    {

        base.Exit();


    }
    public override void Update()
    {
        base.Update();

        time_recorder += Time.deltaTime;

        if (DoesPlayerEscapeFromTheAttackArea())
        {
            Debug.Log("The player escaped! Stop firing.");
            missile_counter_per_attack_round = 0;
            stateMachine.ChangeState(longRangeAttackEnemy.idleState);
            return;
        }

        if (time_recorder >= longRangeAttackEnemy.fireRate || missile_counter_per_attack_round == 0)
        {
            if (missile_counter < 2)
            {
                longRangeAttackEnemy.Fire(longRangeAttackEnemy.TimePauseMissile);
            }
            else
            {
                longRangeAttackEnemy.Fire(longRangeAttackEnemy.HomingMissile);
            }
            missile_counter++;
            missile_counter_per_attack_round++;
            time_recorder = 0f;
        }
    }

    public bool DoesPlayerEscapeFromTheAttackArea()
    {
        float dis = Vector2.Distance(longRangeAttackEnemy.target.transform.position, longRangeAttackEnemy.transform.position);
        if (dis > longRangeAttackEnemy.escapeCircleRadius)
        {
            Debug.Log("The player is missing.");
            return true;
        }
        return false;
    }
}
