using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb { get; private set; }
    public CinemachineImpulseSource impulseSource;

    [Header("Stats")]
    public int health;
    public bool isVulnerable;
    [Header("PlayerDetection")]
    public GameObject player;
    public SpriteRenderer enemyPrototypeSprite;
    public SpriteRenderer enemyEye;
    public float playerDetectionRangeX;
    public float playerDetectionRangeY;
    public bool showDetectionBox;


    [Header("Movement")]
    public bool facingRight = true;
    public int facingDir = 1;
    public float moveSpeed;

    [Header("Attack")]
    public Weapon weapon;
    public float attackRangeX;
    public float attackRangeY;
    public bool showAttackBox;
    public bool attackOver;

    [Header("OnDamage")]
    public bool isInvincible;
    public float onDamageCoolDown;
    public bool isDamaged;
    

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
    public EnemyState deathState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        moveState = new EnemyMoveState(this, stateMachine, "Move");
        attackState = new EnemyAttackState(this, stateMachine, "Attack");
        u1State = new EnemyU1State(this, stateMachine, "AttackU1");
        deathState = new EnemyDeathState(this, stateMachine, "Death");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
        isInvincible = false;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        health = 100;
        isVulnerable = false;
        stateMachine.Initialize(idleState);



    }

    // Update is called once per frame
    void Update()
    {
        
        stateMachine.currentState.Update();
       

        if (isInvincible)
        {
            enemyPrototypeSprite.color = Color.green;
        }
        else
        {
            enemyPrototypeSprite.color = Color.yellow;
        }
        
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

    public void ForceMove(float speedMultiplier)
    {
        rb.velocity = new Vector2(moveSpeed * speedMultiplier * facingDir, rb.velocity.y);
    }

    public void ForceJump(float speedMultiplier)
    {
        rb.velocity = new Vector2(rb.velocity.x, moveSpeed*speedMultiplier);
    }

    public void checkFacingDir()
    {
        int playerDirection = player.transform.position.x > transform.position.x ? 1 : -1;
        if (playerDirection != facingDir)
        {
            Flip();
        }
        return;
    }

    public (bool inRange, bool inAttackRange) PlayerInRange()
    {
        float distX = Mathf.Abs(player.transform.position.x - transform.position.x);
        float distY = Mathf.Abs(player.transform.position.y - transform.position.y);



        bool playerInRange = distX <= playerDetectionRangeX / 2 && distY <= playerDetectionRangeY / 2;
        bool playerInAttackRange = distX <= attackRangeX / 2 && distY <= attackRangeY / 2;
        return (playerInRange, playerInAttackRange);
    }

    public void OnDamage()
    {
        if (!isInvincible) {
            StartCoroutine(nameof(SetInivincibleDelay));
            CameraShakeManager.instance.CameraShake(impulseSource);
            TimeManager.instance.SlowTime(0.07f, 0.1f);
            if (isVulnerable)
            {

                health = 0;
            }
            else {
                health -= 10;
            }
        }
    }

    public void OnDeadlyDamage()
    {

    }
    

    IEnumerator SetInivincibleDelay()
    {
        //Debug.Log("Iam here");
        isInvincible = true;
        yield return new WaitForSecondsRealtime(onDamageCoolDown);
        isInvincible= false;
    }

    public void StartEyeColorChange(Color startColor, Color endColor, float duration)
    {
        
        StartCoroutine(ChangeColorCoroutine(startColor, endColor, duration));
        
    }
    private IEnumerator ChangeColorCoroutine(Color startColor, Color endColor, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            enemyEye.color = Color.Lerp(startColor, endColor, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        enemyEye.color = endColor;
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

}
