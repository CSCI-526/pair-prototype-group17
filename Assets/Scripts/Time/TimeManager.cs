using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool waiting;
    // Start is called before the first frame update
    public static TimeManager instance;
 
    private bool isStopped = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
}
