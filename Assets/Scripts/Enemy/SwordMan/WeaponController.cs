using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponController : MonoBehaviour
{
    public enum WeaponState
    {
        TrackingTarget,
        TrackingBackOriginator,
        DisFunctioned,
        Idle
    }
    public int hitCounter;
    private List<LerpTrail> lerpTrails;
    public bool canDoDamage = false;
    private CinemachineImpulseSource impulseSource;
    private Rigidbody2D rb;
    public float moveSpeed = 8;
    public float turnSpeed = 130;
    public GameObject target;
    private WeaponState weaponState;
    public Enemy originator;
    public bool trailEndInicator;
    public int trailCounter;
    // Start is called before the first frame update
    void Awake()
    {
       
        hitCounter = 0;
    }
    private void Start()
    {
        lerpTrails = new List<LerpTrail>(GetComponents<LerpTrail>());
        impulseSource = GetComponent<CinemachineImpulseSource>();
        weaponState = WeaponState.Idle;
        rb = GetComponent<Rigidbody2D>();
        originator = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(weaponState)
        {
            case WeaponState.Idle:
                switch (hitCounter)
                {
                    case 0:
                        SetColor(Color.white);
                        Debug.Log("Hit Counter is 0");
                        break;
                    case 1:
                        SetColor(Color.yellow);
                        Debug.Log("Hit Counter is 1");

                        break;
                    case 2:
                        SetColor(Color.red);
                        Debug.Log("Hit Counter is 2");

                        break;

                }
                break;
            case WeaponState.TrackingTarget:
                SetColor(Color.red);
                TrackTarget();
                break;
            case WeaponState.TrackingBackOriginator:
                SetColor(Color.white);
                TrackBack();
                break;
            case WeaponState.DisFunctioned:
                SetColor(Color.gray);
                DisableMovement();
                rb.velocity = new Vector2(0, -2f);
                break;

        }
        
          
    }
    public void StartLerpAtIndex(int index)
    {
        if (index >= 0 && index < lerpTrails.Count)
        {
            lerpTrails[index].StartLerp();
        }
        else
        {
            Debug.LogError("Index out of range for starting LerpTrail.");
        }
    }

    public void AttackTrailPermAOnEnter()
    {
        canDoDamage = true;
        trailEndInicator = false;
        trailCounter = 0;
        StartLerpAtIndex(0);
    }

    public void AttackTrailPermAOnExit()
    {
        canDoDamage = false;
    }
    public void AttackTrailPermAOnUpdate()
    {
        
        //trailEndInicator = false;
        //trailCounter = 0;
        
        if (trailEndInicator)
        {
            
            switch (trailCounter)
            {
                case 1:
                    trailEndInicator = false;
                    StartLerpAtIndex(1);
                    break;
                case 2:
                    trailEndInicator = false;
                    StartLerpAtIndex(2);
                    break;
                case 3:
                    trailEndInicator = false;
                    StartLerpAtIndex(3);
                    break;
                case 4:
                    originator.attackOver = true;
                    return;
                default:
                    return;
            }
        }
        //if (enemy.attackOver && enemy.attackLerpCounter == enemy.maxAttackLerpCount - 2)
        //{
        //    enemy.weaponController.StartLerpAtIndex(2);
        //    enemy.attackOver = false;
        //}
        //if (enemy.attackOver && enemy.attackLerpCounter == enemy.maxAttackLerpCount - 3)
        //{
        //    enemy.weaponController.StartLerpAtIndex(3);
        //    enemy.attackOver = false;
        //}
        //if (enemy.attackOver && enemy.attackLerpCounter == enemy.maxAttackLerpCount - 4)
        //{
        //    stateMachine.ChangeState(enemy.idleState);
        //}
    }

    public void SetColor(Color color)
    {
        foreach (SpriteRenderer c in GetComponentsInChildren<SpriteRenderer>())
        {
            if (c.gameObject.tag == "Weapon")
                c.color = color;

        }
    }

    public void OnParry()
    {
        switch (weaponState)
        {
            case WeaponState.Idle:
                if (canDoDamage)
                {
                    CameraShakeManager.instance.CameraShake(impulseSource);
                    if (hitCounter < 2)
                    {
                        hitCounter++;
                    }
                }
                canDoDamage = false;
                break;
            case WeaponState.TrackingTarget:
                TrackBackOriginator();
                break;
            
            
        }
        
    }

    public void OnJumpParry()
    {
        switch (weaponState)
        {
            case WeaponState.Idle:
                if (canDoDamage)
                {
                    CameraShakeManager.instance.CameraShake(impulseSource);
                    
                }
                canDoDamage = false;
                break;
            case WeaponState.TrackingTarget:
                DisableMovement();
                break;


        }

    }

    private void TrackTarget()
    {
        rb.velocity = transform.up * moveSpeed;


        float crossProductTest = Vector3.Cross(transform.up, (target.transform.position - transform.position)).z;
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

    public void TrackBack()
    {
        rb.velocity = transform.up * moveSpeed;
        rb.angularVelocity = 0;
    }

    public void DisableMovement()
    {
        if (weaponState == WeaponState.DisFunctioned)
        {
            return;
        }
        weaponState = WeaponState.DisFunctioned;
        rb.gravityScale = 1;
        CameraShakeManager.instance.CameraShake(impulseSource);
        //TimeManager.instance.PauseTime(onHitPauseDuration);
        //Destroy(gameObject, 1f);
    }



    public void TrackBackOriginator()
    {
        if (weaponState == WeaponState.TrackingBackOriginator || weaponState == WeaponState.DisFunctioned)
        {
            return;
        }
        weaponState = WeaponState.TrackingBackOriginator;
        Vector3 targetDir = originator.transform.position - transform.position;
        if (Vector3.Dot(target.transform.right, targetDir) < 0)
        {
            transform.up = target.transform.right;
        }
        else
        {
            transform.up = targetDir;
        }
        //CameraShakeManager.instance.CameraShake(impulseSource);
        //TimeManager.instance.PauseTime(onHitPauseDuration);
    }


    public void StartTracking()
    {
        weaponState = WeaponState.TrackingTarget;
    }

}
