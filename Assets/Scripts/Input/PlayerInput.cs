using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public InputActions inputActions;
    public bool Jump => inputActions.GamePlay.Jump.WasPressedThisFrame();
    public bool isJumpBuffered { get; set; }
    public float jumpBufferTimeWindow;
    WaitForSeconds jumpBufferTime;
    public bool Roll => inputActions.GamePlay.Roll.WasPressedThisFrame();
    public bool isRollBuffered { get; set; }
    public float rollBufferTimeWindow;
    WaitForSeconds rollBufferTime;
    public bool Attack => inputActions.GamePlay.Attack.WasPressedThisFrame();
    public bool isAttackBuffered { get; set; }
    public float attackBufferTimeWindow;
    WaitForSeconds attackBufferTime;
    public Vector2 AxesInput => inputActions.GamePlay.Move.ReadValue<Vector2>();
    public float Xinput => AxesInput.x;
    public float Yinput => AxesInput.y;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            inputActions = new InputActions();
            jumpBufferTime = new WaitForSeconds(jumpBufferTimeWindow);
            rollBufferTime = new WaitForSeconds(rollBufferTimeWindow);
            attackBufferTime = new WaitForSeconds(attackBufferTimeWindow);
        }
        
        
    }
    public void DisableGamePlayInputs()
    {
        inputActions.GamePlay.Disable();
    }

    public void EnableGamePlayInputs()
    {
        inputActions.GamePlay.Enable();
    }
    public void SetJumpBufferTimer()
    {
        StopCoroutine(nameof(JumpBufferCoroutine));
        StartCoroutine(nameof(JumpBufferCoroutine));
    }
    public void SetRollBufferTimer()
    {
        StopCoroutine(nameof(RollBufferCoroutine));
        StartCoroutine(nameof(RollBufferCoroutine));
    }
    public void SetAttackBufferTimer()
    {
        StopCoroutine(nameof(AttackBufferCoroutine));
        StartCoroutine(nameof(AttackBufferCoroutine));
    }
    IEnumerator JumpBufferCoroutine()
    {
        isJumpBuffered = true;
        yield return jumpBufferTime;
        isJumpBuffered = false;
    }
    IEnumerator RollBufferCoroutine()
    {
        isRollBuffered = true;
        yield return rollBufferTime;
        isRollBuffered = false;
    }
    IEnumerator AttackBufferCoroutine()
    {
        isAttackBuffered = true;
        yield return attackBufferTime;
        isAttackBuffered = false;
    }
}
