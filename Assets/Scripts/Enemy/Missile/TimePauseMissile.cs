//using Cinemachine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem.LowLevel;

public class TimePauseMissile : InteractableProjectile
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
    public GameObject target;
    private Player player;
    private Rigidbody2D rb;
    public float lifeTime;
    private float lifeTimer = 0f;
    private SpriteRenderer missileBodyRenderer;
    private SpriteRenderer missileTipRenderer;
    private CinemachineImpulseSource impulseSource;
    //private TimeManager timeManager;
    private bool isTimeStopped = false;

    private static bool isTestingJumpKey = false;
    private static bool isTestingParryKey = false;
    private static bool isJumpKeyTested = false;
    private static bool isParryKeyTested = false;

    private static int counter = 0;
    private int number = 0;
    private bool visited = false;
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
        number = ++counter;

        if (typeof(Player) != null) player = target.GetComponent<Player>();
    }

    //public void StopAndRequestPressingJumpKey()
    //{
    //    // If time is not stopped and the timePauseMissile is in the area of parry, stop time!
    //    if (!isTimeStopped && IsInTheAreaOfJumpping() && counter % 2 == 0)
    //    {
    //        TimeManager.instance.ToggleTimeStop();
    //        isTimeStopped = !isTimeStopped;
    //        counter++;
    //    }
    //    // If time is stopped and the Jump key is pressed, resume time.
    //    if (isTimeStopped && PlayerInput.instance.Jump && counter % 2 == 1)
    //    {
    //        TimeManager.instance.ToggleTimeStop();
    //        isTimeStopped = !isTimeStopped;
    //    }
    //}

    public bool IsInTheBox(Vector2 centerOffset, float boxHeight, float boxWidth)
    {
        Vector2 boxCenter = (Vector2)target.transform.position + centerOffset;
        Vector2 boxTopLeftCorner = boxCenter - new Vector2(boxWidth / 2, -boxHeight / 2);
        Vector2 boxBottomRightCorner = boxCenter + new Vector2(boxWidth / 2, -boxHeight / 2);
        Collider2D[] colliders = Physics2D.OverlapAreaAll(boxTopLeftCorner, boxBottomRightCorner, player.canBeJumpParried);
        return colliders.Contains(GetComponent<Collider2D>());
    }

    public void StopAndReuqestPressingXXXKey(KeyCode key, Vector2 centerOffset, float boxHeight, float boxWidth)
    {
        if (missileState != MissileState.TrackingTarget) return;
        // If time is not stopped and the timePauseMissile is in the area of parry, stop time!
        if (!isTimeStopped && IsInTheBox(centerOffset, boxHeight, boxWidth))
        {
            Debug.Log("Stop time.");
            Debug.Log($"MissileStateBef: {missileState.ToString()}");
            TimeManager.instance.ToggleTimeStop();
            isTimeStopped = !isTimeStopped;
            if (key == KeyCode.Space) { isTestingJumpKey = true; }
            else if (key == KeyCode.J) { isTestingParryKey = true; }
        }
        // If time is stopped and the Jump key is pressed, resume time.
        if (isTimeStopped && Input.GetKeyDown(key))
        {
            Debug.Log("Resume time.");
            Debug.Log($"MissileStateAft: {missileState.ToString()}");
            TimeManager.instance.ToggleTimeStop();
            isTimeStopped = !isTimeStopped;
            if (key == KeyCode.Space)
            {
                isJumpKeyTested = true;
            }
            else if (key == KeyCode.J) { 
                isParryKeyTested = true;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        //StopAndRequestPressingJumpKey();
        Debug.Log($"counter: {counter}");
        // Toss two Time Pause Missiles to test Jump Key and Parry Key.
        if (number == 1)
        {
            Debug.Log($"isTestingJumpKey: {isTestingJumpKey}");
            Debug.Log($"isJumpKeyTested: {isJumpKeyTested}");
            if (!isTestingJumpKey)
            {
                StopAndReuqestPressingXXXKey(KeyCode.Space, player.jumpBoxCenterOffset, player.jumpBoxHeight, player.jumpBoxWidth);
            }
            else
            {
                if (!isJumpKeyTested)
                {
                    StopAndReuqestPressingXXXKey(KeyCode.Space, player.jumpBoxCenterOffset, player.jumpBoxHeight, player.jumpBoxWidth);
                }
            }
        }
        if (number == 2)
        {
            Debug.Log($"isTestingParryKey: {isTestingParryKey}");
            Debug.Log($"isParryKeyTested: {isParryKeyTested}");
            {
                if (isJumpKeyTested && !isTestingParryKey)
                {
                    StopAndReuqestPressingXXXKey(KeyCode.J, player.attackBoxCenterOffset, player.attackBoxHeight, player.attackBoxWidth);
                }
                else if (isTestingParryKey)
                {
                    if (!isParryKeyTested)
                    {
                        StopAndReuqestPressingXXXKey(KeyCode.J, player.attackBoxCenterOffset, player.attackBoxHeight, player.attackBoxWidth);
                    }
                }
            }
        }

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

        //float crossProductTest = Vector3.Cross(transform.right, (target.transform.position - transform.position).normalized).z;
        //// I did not normalized self to target vector since I want missile to be less accurrate when it is close to player, and avoid sqrt
        //if (crossProductTest > 0.2f)
        //{
        //    rb.angularVelocity = turnSpeed;
        //}
        //else if (crossProductTest < -0.2f)
        //{
        //    rb.angularVelocity = -turnSpeed;
        //}
        //else
        //{
        //    rb.angularVelocity = 0;
        //}
        //float maxTurnSpeed = 200f;
        //rb.angularVelocity = Mathf.Clamp(crossProductTest * turnSpeed, -maxTurnSpeed, maxTurnSpeed);


        //rb.velocity = transform.right * moveSpeed;
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

    //public bool IsInTheAreaOfJumpping()
    //{
    //    Vector2 jumpBoxCenter = (Vector2)target.transform.position + target.jumpBoxCenterOffset;
    //    Vector2 jumpBoxTopLeftCorner = jumpBoxCenter - new Vector2(target.jumpBoxWidth / 2, -target.jumpBoxHeight / 2);
    //    Vector2 jumpBoxBottomRightCorner = jumpBoxCenter + new Vector2(target.jumpBoxWidth / 2, -target.jumpBoxHeight / 2);
    //    Collider2D[] colliders = Physics2D.OverlapAreaAll(jumpBoxTopLeftCorner, jumpBoxBottomRightCorner, target.canBeJumpParried);

    //    return colliders.Contains(GetComponent<Collider2D>());
    //}

    //public bool IsInTheAreaOfParrying()
    //{
    //    Vector2 attackBoxCenter = (Vector2)target.transform.position + target.attackBoxCenterOffset;
    //    Vector2 attackBoxTopLeftCorner = jumpBoxCenter - new Vector2(target.jumpBoxWidth / 2, -target.attackBoxHeight / 2);
    //    Vector2 attackBoxBottomRightCorner = jumpBoxCenter + new Vector2(target.jumpBoxWidth / 2, -target.attackBoxHeight / 2);
    //    Collider2D[] colliders = Physics2D.OverlapAreaAll(jumpBoxTopLeftCorner, jumpBoxBottomRightCorner, target.canBeJumpParried);

    //    return colliders.Contains(GetComponent<Collider2D>());
    //}
}
