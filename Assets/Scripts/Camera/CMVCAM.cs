using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CMVCAM : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();

        vCam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        vCam.m_Lens.OrthographicSize = 13;
    }

    void Start()
    {
        
         
    }

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnLevelLoaded;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnLevelLoaded;
    //}

    //private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    vCam.enabled = false;
    //    vCam.enabled = true; //only way I found to recenter the virtual camera on the player each time a new scene loads up
    //}


}
