using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public enum TutorialType
    {
        Attack,
        Jump,
        None
    }
    public enum Mode
    {
        Track,
        laser
    }
    [Header("baisc info")]
    public TutorialType tutorial = TutorialType.None;
    public Mode mode = Mode.Track;
    public Entity enemy;
    public Player player; // assign from inspector
    public Rigidbody2D rb;
    public bool canDoDamage;
    public float lifeTime = 4f;
    
    public float moveSpeed;
    public float turnSpeed;
    private Collider2D cldr;
    public LayerMask attackDetectionLayer;
    public bool canDoDamageToEnemy;
    public bool isStuckedOnEnemy;
    public CinemachineImpulseSource impulseSource;

    public SpriteRenderer bodySprite;
    public SpriteRenderer tipSprite;
    


    #region States
    public ArrowStateMachine stateMachine { get; private set; }
    public ArrowState trackingState { get; private set; }
    public ArrowState trackBackState { get; private set; } 
    public ArrowState disFunctionState { get; private set; }

    #endregion
    private void Awake()
    {
        stateMachine = new ArrowStateMachine();
        trackingState = new ArrowTrackingState(this, stateMachine);
        trackBackState = new ArrowTrackBackState(this, stateMachine);
        disFunctionState = new ArrowDisFunctionState(this, stateMachine);


       

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cldr = GetComponent<Collider2D>();
        //if (enemy == null)
        //{
        //    enemy = GetComponentInParent<Enemy>();
        //}
        canDoDamage = true;
        
        rb.gravityScale = 0;
        canDoDamageToEnemy = false;
        isStuckedOnEnemy = false;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        stateMachine.Initialize(trackingState);
        DestroyMeOnLifeOver();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();
    }

    public void TrackTarget()
    {
        rb.velocity = transform.right * moveSpeed;


        float crossProductTest = Vector3.Cross(transform.right, (player.transform.position - transform.position)).z;
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

        Vector3 targetDir = enemy.transform.position - transform.position;
        if (Vector3.Dot(player.transform.right, targetDir) < 0)
        {
            transform.right = player.transform.right;
        }
        else
        {
            transform.right = targetDir;
        }

    }

    public void OnParry()
    {
        stateMachine.currentState.OnParry();
    }
    public void OnJumpParry()
    {
        stateMachine.currentState.OnJumpParry();
    }
    
    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    public void SetColor(Color color)
    {
        bodySprite.color = color;
        tipSprite.color = color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (tutorial)
        {
            case TutorialType.None:
                break;
            case TutorialType.Attack:
                if (other.CompareTag("PlayerAtkBox"))
                {
                    TimeManager.instance.PauseUntilJPressed();
                }
                break;
            case TutorialType.Jump:
                if (other.CompareTag("PlayerJmpBox"))
                {
                    TimeManager.instance.PauseUntilSpacePressed();
                }
                break;
        }     
    }

    public void DestroyMeOnLifeOver()
    {
        StartCoroutine(nameof(DestroyMeCoroutine));
    }
    IEnumerator DestroyMeCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
