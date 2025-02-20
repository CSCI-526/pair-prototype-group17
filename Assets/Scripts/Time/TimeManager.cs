using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool waiting;
    // Start is called before the first frame update
    public static TimeManager instance;
 
    private bool isStopped = false;
    public TextMeshProUGUI attText;
    public TextMeshProUGUI jmpText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    private void Start()
    {
        if (attText != null)
        {
            attText.gameObject.SetActive(false);
        }
        if (jmpText != null)
        {

            jmpText.gameObject.SetActive(false);
        }
    }
    public void SlowTime(float pauseDuration, float scale)
    {   if (waiting)
        {
            return;
        }
        Time.timeScale = scale;
        StartCoroutine(Wait(pauseDuration));
    }
    IEnumerator Wait(float pauseDuration)
    {
        waiting = true;
        //CinemachineImpulseManager.Instance.IgnoreTimeScale = true;
        //Camera.main.GetComponent<CinemachineBrain>().m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;
        //Camera.main.GetComponent<CinemachineBrain>().m_IgnoreTimeScale = true;
        yield return new WaitForSecondsRealtime(pauseDuration);
        Time.timeScale = 1.0f;
        //CinemachineImpulseManager.Instance.IgnoreTimeScale = false;
        //Camera.main.GetComponent<CinemachineBrain>().m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
        //Camera.main.GetComponent<CinemachineBrain>().m_IgnoreTimeScale = false;
        waiting = false;
    }
    
    // Stop or continue time.
    public void ToggleTimeStop() {
        if (isStopped) {
            // Resume time when isStopped = true;
            SetTimeScale(1);
            //Time.fixedDeltaTime = 0.02f * Time.timeScale;
        } else
        {
            // Stop time when isStopped = false;
            SetTimeScale(0);
            //Time.fixedDeltaTime = 0.02f;
        }
        isStopped = !isStopped;
    }


    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public void PauseUntilJPressed()
    {
        StartCoroutine(nameof(PauseUntilJPressedCoroutine));
    }

    IEnumerator PauseUntilJPressedCoroutine()
    {
        if (attText != null)
        {
            attText.gameObject.SetActive(true);
        }
        Time.timeScale = 0;
        yield return new WaitUntil(() => PlayerInput.instance.Attack);
        Time.timeScale = 1.0f;
        if (attText != null)
        {
            attText.gameObject.SetActive(false);
        }
    }

    public void PauseUntilSpacePressed()
    {
        StartCoroutine(nameof(PauseUntilSpacePressedCoroutine));
    }

    IEnumerator PauseUntilSpacePressedCoroutine()
    {
        if (jmpText != null)
        {

            jmpText.gameObject.SetActive(true);
        }
        Time.timeScale = 0;
        yield return new WaitUntil(() => PlayerInput.instance.Jump);
        Time.timeScale = 1.0f;
        if (jmpText != null)
        {

            jmpText.gameObject.SetActive(false);
        }
    }
}
