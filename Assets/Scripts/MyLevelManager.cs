using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLevelManager : MonoBehaviour
{
    public static MyLevelManager instance;// Start is called before the first frame update
    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        
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

        PlayerInput.instance.DisableGamePlayInputs();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void ResetCurrentScene()
    {
        PlayerInput.instance.DisableGamePlayInputs();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
