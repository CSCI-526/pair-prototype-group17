using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMissile : InteractableProjectile
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
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        switch (missileState)
        {
            case MissileState.TrackingTarget:
                SetColor(Color.red);
                rb.velocity = transform.right * moveSpeed;
                break;
            case MissileState.TrackingBackOriginator:
                SetColor(Color.yellow);
                rb.velocity = transform.right * moveSpeed;
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


    public override void DisableMovement()
    {
        if (missileState == MissileState.DisFunctioned)
        {
            return;
        }
        missileState = MissileState.DisFunctioned;
        rb.gravityScale = 1;
    }

    
    public override void TrackBackOriginator()
    {
        if (missileState == MissileState.TrackingBackOriginator)
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
