using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class TimePauseMissle : InteractableProjectile
{
    public enum MissileState
    {
        TrackingTarget,
        TrackingBackOriginator,
        DisFunctioned
    }
    public GameObject originator;
    public MissileState missileState;
    public float moveSpeed;
    public float turnSpeed;
    public float onHitPauseDuration;
    public Player target;
    private Rigidbody2D rb;
    public float lifeTime;
    private float lifeTimer = 0f;
    private SpriteRenderer missileBodyRenderer;
    private SpriteRenderer missileTipRenderer;
    private CinemachineImpulseSource impulseSource;
    //private TimeManager timeManager;
    private bool isTimeStopped = false;
    private int counter = 0;
    //private 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        missileBodyRenderer = GetComponent<SpriteRenderer>();
        missileTipRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        missileState = MissileState.TrackingTarget;
        lifeTimer = lifeTime;
        rb.gravityScale = 0;

    }

    public void StopAndRequestPressingJumpKey()
    {
        // If time is not stopped and the timePauseMissle is in the area of parry, stop time!
        if (!isTimeStopped && IsInTheAreaOfJumpping() && counter % 2 == 0)
        {
            TimeManager.instance.ToggleTimeStop();
            isTimeStopped = !isTimeStopped;
            counter++;
        }
        // If time is stopped and the Jump key is pressed, resume time.
        if (isTimeStopped && PlayerInput.instance.Jump && counter % 2 == 1)
        {
            TimeManager.instance.ToggleTimeStop();
            isTimeStopped = !isTimeStopped;
        }
    }

    public void StopAndReuqestPressingXXXKey(KeyCode key)
    {
        // If time is not stopped and the timePauseMissle is in the area of parry, stop time!
        if (!isTimeStopped && IsInTheAreaOfJumpping() && counter % 2 == 0)
        {
            Debug.Log("Stop time.");
            TimeManager.instance.ToggleTimeStop();
            isTimeStopped = !isTimeStopped;
            counter++;
        }
        // If time is stopped and the Jump key is pressed, resume time.
        if (isTimeStopped && Input.GetKeyDown(key) && counter % 2 == 1)
        {
            Debug.Log("Resume time.");
            TimeManager.instance.ToggleTimeStop();
            isTimeStopped = !isTimeStopped;
        }
    }



    public void ResetCounter()
    {
        counter = 0;
    }


    // Update is called once per frame
    void Update()
    {
        StopAndRequestPressingJumpKey();
        //StopAndReuqestPressingXXXKey(KeyCode.Space);

        lifeTimer -= Time.deltaTime;
        switch (missileState)
        {
            case MissileState.TrackingTarget:
                //SetColor(Color.red);
                TrackTarget();
                break;
            case MissileState.TrackingBackOriginator:
                //SetColor(Color.yellow);
                TrackBack();
                break;
            case MissileState.DisFunctioned:
                SetColor(Color.gray);
                DisableMovement();
                break;
        }


        if (lifeTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void TrackTarget()
    {
        rb.velocity = transform.right * moveSpeed;


        float crossProductTest = Vector3.Cross(transform.right, (target.transform.position - transform.position)).z;
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
        rb.velocity = transform.right * moveSpeed;
        rb.angularVelocity = 0;
    }

    public override void DisableMovement()
    {
        if (missileState == MissileState.DisFunctioned)
        {
            return;
        }
        missileState = MissileState.DisFunctioned;
        rb.gravityScale = 1;
        CameraShakeManager.instance.CameraShake(impulseSource);
        //TimeManager.instance.PauseTime(onHitPauseDuration);
        //Destroy(gameObject, 1f);
    }



    public override void TrackBackOriginator()
    {
        if (missileState == MissileState.TrackingBackOriginator || missileState == MissileState.DisFunctioned)
        {
            return;
        }
        missileState = MissileState.TrackingBackOriginator;
        Vector3 targetDir = originator.transform.position - transform.position;
        if (Vector3.Dot(target.transform.right, targetDir) < 0)
        {
            transform.right = target.transform.right;
        }
        else
        {
            transform.right = targetDir;
        }
        CameraShakeManager.instance.CameraShake(impulseSource);
        //TimeManager.instance.PauseTime(onHitPauseDuration);
    }

    private void SetColor(Color color)
    {
        missileBodyRenderer.color = color;
        missileTipRenderer.color = color;
    }

    public bool IsInTheAreaOfJumpping()
    {
        //Vector2 playerSize = target.GetBounds();
        //float playerWidth = playerSize.x;
        //float playerHeight = playerSize.y;
        Vector2 jumpBoxCenter = (Vector2)target.transform.position + target.jumpBoxCenterOffset;
        Vector2 jumpBoxTopLeftCorner = jumpBoxCenter - new Vector2(target.jumpBoxWidth / 2, -target.jumpBoxHeight / 2);
        //Vector2 jumpBoxTopLeftCorner = jumpBoxCenter - new Vector2(target.jumpBoxWidth / 2, playerHeight / 2);
        Vector2 jumpBoxBottomRightCorner = jumpBoxCenter + new Vector2(target.jumpBoxWidth / 2, -target.jumpBoxHeight / 2);
        //Vector2 jumpBoxBottomRightCorner = jumpBoxCenter + new Vector2(target.jumpBoxWidth / 2, target.jumpBoxHeight - playerHeight / 2);
        Collider2D[] colliders = Physics2D.OverlapAreaAll(jumpBoxTopLeftCorner, jumpBoxBottomRightCorner, target.canBeJumpParried);

        return colliders.Contains(GetComponent<Collider2D>());
    }


}
