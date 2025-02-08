using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    
    private void AnimationOver()
    {
        player.stateMachine.currentState.AnimationOverEvent();
    }
}
