using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class KeyPromptTextBox : MonoBehaviour
{
    public Vector3 keyPrompttextBoxoffset = new Vector3(0, 3.0f, 0);
    public TextMeshProUGUI keyPromptTextBox;
    private string[] messages = { "Press 'Space' to jump parry", "Press 'J' to parry" };
    
    public GameObject player;
    private Camera cam;
    private int playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        keyPromptTextBox.gameObject.SetActive(false);
        playerLayer = LayerMask.NameToLayer("Player");

        GameObject[] objectsInPlayerLayer = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objectsInPlayerLayer)
        {
            if (obj.layer != playerLayer)
            {
                player = obj;
                break;
            }
        }
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMessage();
        PlaceTheTextAboveThePlayer();
    }

    public void PlaceTheTextAboveThePlayer()
    {
        keyPromptTextBox.transform.position = cam.WorldToScreenPoint(player.transform.position + keyPrompttextBoxoffset);
        if (Time.deltaTime == 0)
        {
            keyPromptTextBox.gameObject.SetActive(true);
        } else
        {
            keyPromptTextBox.gameObject.SetActive(false);
        }
     
    }

    public void ChangeMessage() {
        if (TimePauseMissile.counter == 1 || TimePauseMissile.counter == 2)  keyPromptTextBox.text = messages[TimePauseMissile.counter - 1];
    }
}
