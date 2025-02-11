using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLancher : MonoBehaviour
{
    public GameObject missilePrefab;
    public float lauchInterval = 5f;
    private float lauchTimer;
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
        InteractableProjectile missile = missileInstance.GetComponent<InteractableProjectile>();
        if (missile != null)
        {
            if (missile is HomingMissile homing)
            {
                
                homing.target = target;
                homing.moveSpeed = missileSpeed;
                homing.turnSpeed = missileTurnSpeed;
                homing.lifeTime = missileLifeTime;
                homing.originator = gameObject;
            }
            else if (missile is LaserMissile laser)
            {
                laser.target = target;
                laser.moveSpeed = missileSpeed;
                laser.lifeTime = missileLifeTime;
                laser.originator = gameObject;
            }
            
        }
    }
}
