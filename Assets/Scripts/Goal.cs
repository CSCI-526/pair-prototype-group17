using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D cldr;
    void Start()
    {
        cldr = GetComponent<Collider2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("ddd");
        if (other.GetComponent<Player>() != null) {
            MyLevelManager.instance.LoadNextLevel();
        }

    }
}
