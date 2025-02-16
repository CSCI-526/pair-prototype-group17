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
}
