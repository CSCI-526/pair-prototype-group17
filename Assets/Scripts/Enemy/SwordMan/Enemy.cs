using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public LerpTrail lerpTrail;
    public Rigidbody2D rb { get; private set; }

    [Header("PlayerDetection")]
    public GameObject player;
    public float playerDetectionRangeX;
    public float playerDetectionRangeY;
    public bool showDetectionBox;


    [Header("Movement")]
    public bool facingRight = true;
    public int facingDir = 1;
    public float moveSpeed;

    [Header("Attack")]
    public WeaponController weaponController;
    public float attackRangeX;
    public float attackRangeY;
    public bool showAttackBox;
    public int attackLerpCounter;
    public int maxAttackLerpCount;
    public bool attackOver;

    [Header("Collision")]
    public Transform groundCheck;
    public Transform groundCheckFront;
    public float grounCheckDistance;
    public LayerMask whatIsGround;
    public Transform wallCheckTop;
    public Transform wallCheckBottom;
    public float wallCheckDistance;
    public LayerMask whatIsWall;

    #region States
    public EnemyStateMachine stateMachine { get; private set; }
    public EnemyState idleState { get; private set; }
    public EnemyState moveState { get; private set; }
    public EnemyState attackState { get; private set; }
    public EnemyState u1State { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        moveState = new EnemyMoveState(this, stateMachine, "Move");
        attackState = new EnemyAttackState(this, stateMachine, "Attack");
        u1State = new EnemyU1State(this, stateMachine, "AttackU1");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
        weaponController = GetComponentInChildren<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    lerpTrail.StartLerp();
        //}
    }

    public bool IsGroundDetected()
    {
        bool groundCheck1 = Physics2D.Raycast(groundCheck.position, Vector2.down, grounCheckDistance, whatIsGround);
        bool groundCheck2 = Physics2D.Raycast(groundCheckFront.position, Vector2.down, grounCheckDistance, whatIsGround);
        return groundCheck1 && groundCheck2;
    }
    public bool IsWallDetected()
    {

        bool topCheck = Physics2D.Raycast(wallCheckTop.position, Vector2.right * facingDir, wallCheckDistance, whatIsWall);
        bool bottomCheck = Physics2D.Raycast(wallCheckBottom.position, Vector2.right * facingDir, wallCheckDistance, whatIsWall);
        return topCheck && bottomCheck;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - grounCheckDistance));
        Gizmos.DrawLine(groundCheckFront.position, new Vector3(groundCheckFront.position.x, groundCheckFront.position.y - grounCheckDistance));
        Gizmos.DrawLine(wallCheckTop.position, new Vector3(wallCheckTop.position.x + wallCheckDistance * facingDir, wallCheckTop.position.y));
        Gizmos.DrawLine(wallCheckBottom.position, new Vector3(wallCheckBottom.position.x + wallCheckDistance * facingDir, wallCheckBottom.position.y));
        if (showDetectionBox)
        {
            
            Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y), new Vector3(playerDetectionRangeX, playerDetectionRangeY, 0));

        }
        if (showDetectionBox)
        {

            Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y), new Vector3(attackRangeX, attackRangeY, 0));

        }


        //Gizmos.DrawWireCube((Vector2)transform.position + attackBoxCenterOffset, new Vector3(attackBoxWidth, attackBoxHeight,  0));
    }

    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
        else
        {

        }
    }
}
