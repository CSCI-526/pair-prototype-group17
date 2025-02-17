using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public PlayerInput input;

    [Header("Stats")]
    public SpriteRenderer playerPrototypeSprite;
    public float invincibleTimer;
    public int frameRate = 60;
    public int health;
    public PlayerHealth playerHealthUI;
    
    [Header("Movement")]
    public float moveSpeed;
    public float stopSpeed;
    public float acceleration;
    [SerializeField] private Vector2 rawSpeed;
    public int facingDir { get; private set; } = 1;
    public bool facingRight = true;

    [Header("Jump")]
    public float jumpSpeed;
    public float jumpXSpeedMultiplier;
    public int jumpCounter;
    public int maxJumpsAllowed;
    public float jumpParryWindow;
    public float jumpParryInivicibleTimeWindow;
    public GameObject jumpBoxIndicator;
    public Vector2 jumpBoxCenterOffset;
    public float jumpBoxWidth;
    public float jumpBoxHeight;
    public LayerMask canBeJumpParried;
    public LayerMask canBeJumpParriedWeapon;
    public bool showJumpBox;


    [Header("Roll")]
    public float rollTimer;
    public float rollCooldown;
    public float rollSpeed;
    public float rollDirection;
    public float rollDuration; // use when no animation event allowed
    public float rollInvicibleTimeWindow;

    [Header("Dash")]
    public int dashCounter;
    public float dashDirection;
    public float dashSpeed;
    public int maxDashesAllowed;
    public float dashDuration; // use when no animation event allowed
    public float dashInvicibleTimeWindow;

    [Header("WallSlide")]
    public float wallSlideTimer;
    public float wallSlideCoolDown;
    public float wallSlideSpeed;
    public float wallSlideMinDuration;

    [Header("WallJump")]
    public float wallJumpFreezeTimer;
    public float wallJumpFreezeCoolDown;
    public float wallJumpXSpeed;

    [Header("Attack")]
    public float attackTimer;
    public float attackCoolDown;
    public float attackJumpForce;
    public float attackDuration; // use if no animation event allowed
    public GameObject attackIndicator; // use if no animation event allowed
    public Vector2 attackBoxCenterOffset;
    public float attackBoxWidth;
    public float attackBoxHeight;
    public bool canDoDamage;
    public LayerMask canBeAttacked; // layer for enemy 
    public LayerMask canBeAttackParried; // layer for deflectable projectile
    public LayerMask canBeAttackParriedWeapon; // layer for deflectable weapon
    public bool showAttackBox;
    public float attackInvicibleTimeWindow;

    [Header("OnDamage")]
    public Vector2 knockBackForce; // this should be set using OnDamage function, direction is the same as knock force.
    public bool isDamaged;
    public CinemachineImpulseSource impulseSource;
    public float OnDamageCoolDown;

    [Header("Collision")]
    public Transform groundCheck;
    public float grounCheckDistance;
    public LayerMask whatIsGround;
    public Transform wallCheckTop;
    public Transform wallCheckBottom;
    public float wallCheckDistance;
    public LayerMask whatIsWall;

    
    [Header("Animation")]
    [SerializeField] private string animState;



    #region Components

    [Header("Components")]
    public Animator anim;
    public Animator animWeapon;
    public Rigidbody2D rb { get; private set; }
    #endregion


    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerState idleState { get; private set; }
    public PlayerState moveState { get; private set; }
    public PlayerState jumpState { get; private set; }
    public PlayerState airState { get; private set; }
    public PlayerState rollState { get; private set; }
    public PlayerState wallSlideState { get; private set; }
    public PlayerState wallJumpState { get; private set; }
    public PlayerState dashState { get; private set; }
    public PlayerState attackState { get; private set; }
    public PlayerState onDamageState { get; private set; }
    #endregion
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        rollState = new PlayerRollState(this, stateMachine, "Roll");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        onDamageState = new PlayerOnDamageState(this, stateMachine, "OnDamage");

        input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        //anim = GetComponentInChildren<Animator>(); // set in inspector
        Application.targetFrameRate = frameRate;
        rb = GetComponent<Rigidbody2D>();
        isDamaged = false;
        input.EnableGamePlayInputs();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        health = 100;
        playerHealthUI = GetComponent<PlayerHealth>();
        stateMachine.Initialize(idleState);
        
       
        
    }

    private void Update()
    {
        rawSpeed = rb.velocity;
        // set buffer before state update
        if (input.Jump)
        {
            input.SetJumpBufferTimer();
        }
        if (input.Roll)
        {
            input.SetRollBufferTimer();
        }
        if (input.Attack)
        {
            input.SetAttackBufferTimer();
        }

        // global timers before state update
        rollTimer -= Time.deltaTime;
        wallSlideTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        invincibleTimer -= Time.deltaTime;

        // state update
        stateMachine.currentState.Update();

        // debug info after state update
        if (invincibleTimer > 0)
        {
            playerPrototypeSprite.color = Color.yellow;
        }
        else
        {
            if (isDamaged)
            {
                playerPrototypeSprite.color = Color.red;
            }
            else
            {
                playerPrototypeSprite.color = Color.gray;

            }
        }
        playerHealthUI.health = health;
        
        

        //if (rollTimer<0 && Input.GetKeyDown(KeyCode.L))
        //{
        //    rollTimer = rollCooldown;
        //}
    }

    private void LateUpdate()
    {
        stateMachine.currentState.LateUpdate();
    }

    public void ChangeAnimationState(string newState)
    {
        if (animState == newState) return;
        anim.Play(newState);
        animState = newState;
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, grounCheckDistance, whatIsGround);
    public bool IsWallDetected(){
       
        bool topCheck = Physics2D.Raycast(wallCheckTop.position, Vector2.right * facingDir, wallCheckDistance, whatIsWall);
        bool bottomCheck = Physics2D.Raycast(wallCheckBottom.position, Vector2.right * facingDir, wallCheckDistance, whatIsWall);
        return topCheck && bottomCheck;
    } 

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - grounCheckDistance)); 
        Gizmos.DrawLine(wallCheckTop.position, new Vector3(wallCheckTop.position.x + wallCheckDistance * facingDir, wallCheckTop.position.y));
        Gizmos.DrawLine(wallCheckBottom.position, new Vector3(wallCheckBottom.position.x + wallCheckDistance * facingDir, wallCheckBottom.position.y));
        if (showJumpBox)
        {     
            float boxCenterX = transform.position.x + jumpBoxCenterOffset.x;
            float boxCenterY = transform.position.y + jumpBoxCenterOffset.y;
            Gizmos.DrawWireCube(new Vector2(boxCenterX, boxCenterY), new Vector3(jumpBoxWidth, jumpBoxHeight, 0));
          
        }
        if (showAttackBox)
        {
            if (facingRight)
            {
                float boxCenterX = transform.position.x + attackBoxCenterOffset.x;
                float boxCenterY = transform.position.y + attackBoxCenterOffset.y;
                Gizmos.DrawWireCube(new Vector2(boxCenterX, boxCenterY), new Vector3(attackBoxWidth, attackBoxHeight, 0));
            }
            else
            {
                float boxCenterX = transform.position.x - attackBoxCenterOffset.x;
                float boxCenterY = transform.position.y + attackBoxCenterOffset.y;
                Gizmos.DrawWireCube(new Vector2(boxCenterX, boxCenterY), new Vector3(attackBoxWidth, attackBoxHeight, 0));
            }
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
        if (_x>0 && !facingRight)
        {
            Flip();
        }else if (_x<0 && facingRight)
        {
            Flip();
        }
        else
        {

        }
    }

    void OnGUI()
    {
        //GUI.Label(new Rect(200, 200, 200, 200), "isJumpBuffered: " + input.isJumpBuffered);
        //GUI.Label(new Rect(200, 220, 200, 200), "isRollBuffered: " + input.isRollBuffered);
        //GUI.Label(new Rect(200, 240, 200, 200), "isAttackBuffered: " + input.isAttackBuffered);
    }

    public void OnHit(Transform targetTransform, float magnitude, bool fixVertical)
    {
        Vector2 dir;
        if (fixVertical)
        {
            float xdist = targetTransform.position.x > transform.position.x ? -1 : 1;
            dir = new Vector2(xdist, 0);
        }
        else
        {
            dir = (targetTransform.position - transform.position).normalized;
            
        }
        knockBackForce = dir * magnitude;
    }

    public bool OnDamage()
    {
        if (!isDamaged && invincibleTimer<0)
        {
            isDamaged = true;
            return true;
        }
        return false;
    }

    // Get the size of the player.
    //public Vector2 GetBounds()
    //{
    //    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    //    return spriteRenderer.bounds.size;
    //}

}
