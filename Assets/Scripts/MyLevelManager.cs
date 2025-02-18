using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLevelManager : MonoBehaviour
{
    public static MyLevelManager instance;// Start is called before the first frame update
    public CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        //if (virtualCamera == null)
        //    virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {

        //virtualCamera.enabled = false;
        PlayerInput.instance.DisableGamePlayInputs();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void ResetCurrentScene()
    {
        //virtualCamera.enabled = false;
        PlayerInput.instance.DisableGamePlayInputs();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
