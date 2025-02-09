using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLancher : MonoBehaviour
{
    public GameObject missilePrefab;
    public float lauchInterval = 5f;
    public float lauchTimer;
    public GameObject target;
    public float missileLifeTime = 5f;
    public float missileSpeed = 5f;
    public float missileTurnSpeed = 130f;
    // Start is called before the first frame update
    void Start()
    {
        //lauchTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        lauchTimer -= Time.deltaTime;
        if (lauchTimer < 0)
        {
            LaubchMissile();
            lauchTimer = lauchInterval;
        }

    }

    void LaubchMissile()
    {
        
        GameObject missileInstance = Instantiate(missilePrefab, transform.position, transform.rotation);
        HomingMissile missile = missileInstance.GetComponent<HomingMissile>();
        if (missile != null)
        {
            missile.isFunctioning = true;
            missile.target = target;
            missile.moveSpeed = missileSpeed;
            missile.turnSpeed = missileTurnSpeed;
            missile.lifeTime = missileLifeTime;
        }
    }
}
