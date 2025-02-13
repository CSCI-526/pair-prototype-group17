using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterMissile : InteractableProjectile
{
    // Start is called before the first frame update
    public enum MissileState
    {
        TrackingTarget,
        TrackingBackOriginator,
        DisFunctioned
    }
    public int interactCount;
    [SerializeField] private int interactCounter;
    public GameObject originator;
    public MissileState missileState;
    public float moveSpeed;
    public float turnSpeed;
    public GameObject target;
    private Rigidbody2D rb;
    public float lifeTime;
    private float lifeTimer = 0f;
    private SpriteRenderer missileBodyRenderer;
    private SpriteRenderer missileTipRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        missileBodyRenderer = GetComponent<SpriteRenderer>();
        missileTipRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        missileState = MissileState.TrackingTarget;
        lifeTimer = lifeTime;
        rb.gravityScale = 0;
        interactCounter = interactCount;

    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        switch (missileState)
        {
            case MissileState.TrackingTarget:
                SetColor(Color.red);
                TrackTarget();
                break;
            case MissileState.TrackingBackOriginator:
                SetColor(Color.yellow);
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
    }

    private void SetColor(Color color)
    {
        missileBodyRenderer.color = color;
        missileTipRenderer.color = color;
    }
}
