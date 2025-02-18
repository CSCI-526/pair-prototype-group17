using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Archer : Entity
{
    // Start is called before the first frame update
    [Header("Arrow")]
    public Transform lauchLocation;
    public GameObject arrowPrefab;
    public int tempCounter;
    #region States
    public ArcherStateMachine stateMachine { get; private set; }
    public ArcherState idleState { get; private set; }
    public ArcherState shootState { get; private set; }
    #endregion
    public override void Awake()
    {
        base.Awake();
        stateMachine = new ArcherStateMachine();
        idleState = new ArcherIdleState(this, stateMachine);
        shootState = new ArcherShootState(this, stateMachine);

    }
    public override void Start()
    {
        base.Start();
        tempCounter = 0;
        stateMachine.Initialize(idleState);
    }

    public override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }


    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public void ShootArrow(Arrow.TutorialType type, Arrow.Mode mode)
    {
        lauchLocation.right = (player.transform.position -lauchLocation.position).normalized;
        GameObject instance = Instantiate(arrowPrefab, lauchLocation.position, lauchLocation.rotation);
        Arrow arrow = instance.GetComponent<Arrow>();
        if (arrow != null)
        {
            

            arrow.tutorial = type;
            arrow.mode = mode;
            arrow.enemy = this;
            arrow.player = player;

            
        }
    }



    

    
}
