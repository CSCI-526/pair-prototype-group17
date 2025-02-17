using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using static WeaponController;

public class Weapon : MonoBehaviour
{

    public Rigidbody2D rb;

    [Header("Info")]
    public Enemy enemy;
    public Vector3 InitialPostion = new Vector3(-1.1f, 0, 0 );
    public Vector3 InitialRotation = new Vector3(0,0,0);
    public int hitCounter;
    public bool canDoDamage;
    public bool canBeParried;
    public float moveSpeed;
    public float turnSpeed;
    public Collider2D cldr;
    public LayerMask attackDetectionLayer;

    [Header("Attack")]
    public bool curveDoneSwitch;
    public MoveAlongSpline a1PreCastCurve;
    public MoveAlongSpline a1Curve;
    public MoveAlongSpline a1PostCastCurve;
    public bool a1Switch;
    public MoveAlongSpline u1PreCastCurve;
    public CinemachineImpulseSource impulseSource;
    #region States
    public WeaponStateMachine stateMachine { get; private set; }
    public WeaponState idleState { get; private set; }
    public WeaponState attackState { get; private set; }
    public WeaponState trackState { get; private set; }
    public WeaponState trackBackState { get; private set; }
    public WeaponState brokenState { get; private set; }
    public WeaponState a1PreCastState { get; private set; }
    public WeaponState a1State { get; private set; }
    public WeaponState a1PostCastState { get; private set; }
    public WeaponState u1PreCastState { get; private set; }


    #endregion
    private void Awake()
    {
        stateMachine = new WeaponStateMachine();
        idleState = new WeaponIdleState(this, stateMachine);
        attackState = new WeaponAttackState(this, stateMachine);
        trackState = new WeaponTrackState(this, stateMachine);
        trackBackState = new WeaponTrackBackState(this, stateMachine);
        brokenState = new WeaponBrokenState(this, stateMachine);
        a1PreCastState = new A1PreCastState(this, stateMachine);
        a1State = new A1State(this, stateMachine);
        a1PostCastState = new A1PostCastState(this, stateMachine);
        u1PreCastState = new U1PreCastState(this, stateMachine);

        //enemy = GetComponentInParent<Enemy>();


    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cldr = GetComponent<Collider2D>();
        if (enemy == null)
        {
            enemy = GetComponentInParent<Enemy>();
        }
        a1Switch = false;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        canDoDamage = false;
        canBeParried = false;
        rb.gravityScale = 0;
        //// important!!! put initialize in last line of start if the first state Enter() uses any pointer that is done in start.
        stateMachine.Initialize(idleState);
        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();
        
    }

    public void StartAttack1()
    {
        a1Switch = true;
    }

    public void OnParry()
    {
        stateMachine.currentState.OnParry();
        
        
    }
    public void OnJumpParry()
    {
        stateMachine.currentState.OnJumpParry();
    }
    public void SetColor(Color color)
    {
        foreach (SpriteRenderer c in GetComponentsInChildren<SpriteRenderer>())
        {
            if (c.gameObject.tag == "Weapon")
                c.color = color;

        }
    }

    public void TrackTarget()
    {
        rb.velocity = transform.up * moveSpeed;


        float crossProductTest = Vector3.Cross(transform.up, (enemy.player.transform.position - transform.position)).z;
        // I did not normalized self to target vector since I want missile to be less accurrate when it is close to player, and avoid sqrt
        if (crossProductTest > 0.2f)
        {
            rb.angularVelocity = turnSpeed;
        }
        else if (crossProductTest < -0.2f)
        {
            rb.angularVelocity = -turnSpeed;
        }
        else
        {
            rb.angularVelocity = 0;
        }
    }

    public void TrackBackOriginator()
    {
        
        Vector3 targetDir = enemy.player.transform.position - transform.position;
        if (Vector3.Dot(enemy.player.transform.right, targetDir) < 0)
        {
            transform.up = enemy.player.transform.right;
        }
        else
        {
            transform.up = targetDir;
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (canDoDamage)
        {
            Player player = other.gameObject.GetComponent<Player>();
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (player != null)
            {
                if (player.OnDamage())
                {
                    player.OnHit(transform,8, true);
                }
            }
            if (enemy != null)
            {
                enemy.OnDamage();
            }

        }
    }



}
