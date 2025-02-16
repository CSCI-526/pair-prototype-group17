using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LerpTrail : MonoBehaviour
{
    public TransformProfile startProfile;
    public TransformProfile endProfile;
    public float duration = 1.0f;
    private float speed;
    private float t = 0.0f;
    private bool movingToEnd = false;
    private Vector3 localStartPosition;
    private Vector3 localEndPosition;
    private Quaternion localStartRotation;
    private Quaternion localEndRotation;
    public Enemy enemy;
    public WeaponController weaponController;
    // Start is called before the first frame update
    void Start()
    {
        if (duration > 0)
        {
            speed = 1 / duration;
        }else{
            //Debug.LogError("lerp duration must be greater than zero.");
        }
        UpdateLocation();
        enemy = GetComponentInParent<Enemy>();
        weaponController = GetComponentInParent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLocation();
        if (movingToEnd)
        {
            t += Time.deltaTime * speed;
            if (t > 1.0f)
            {
                t = 1.0f;
                movingToEnd = false;
                //enemy.attackLerpCounter --;
                //enemy.attackOver = true;
                weaponController.trailEndInicator = true;
                weaponController.trailCounter++;

}
            transform.position = Vector3.Lerp(localStartPosition, localEndPosition, t);
            transform.rotation = Quaternion.Slerp(localStartRotation, localEndRotation, t);
        }
    }

    public void StartLerp()
    {
        t = 0.0f;
        movingToEnd = true;
    }

    public void UpdateLocation()
    {
        localStartPosition = transform.parent.TransformPoint(startProfile.position);
        localEndPosition = transform.parent.TransformPoint(endProfile.position);
        localStartRotation = transform.parent.rotation * Quaternion.Euler(startProfile.rotation);
        localEndRotation = transform.parent.rotation * Quaternion.Euler(endProfile.rotation);
    }


}
