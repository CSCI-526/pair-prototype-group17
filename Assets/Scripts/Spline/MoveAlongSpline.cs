using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MoveAlongSpline : MonoBehaviour
{
    

    public SplineContainer spline;
    public float speed = 1.0f;
    private float distancePercentage = 0.0f;
    private float splineLength = 0.0f;
    public Enemy enemy;
    public Weapon weapon;
    private bool canStart;

    // Start is called before the first frame update
    
    void Start()
    {
        splineLength = spline.CalculateLength();
        enemy = GetComponentInParent<Enemy>();
        weapon = GetComponent<Weapon>();
        canStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canStart)
        {
            distancePercentage += speed * Time.deltaTime / splineLength;
            if (distancePercentage > 1f)
            {
                distancePercentage = 0.0f;
                canStart = false;
                weapon.curveDoneSwitch = true;
                return;
            }

            Vector3 currPos = spline.EvaluatePosition(distancePercentage);
            Vector3 currTan = spline.EvaluateTangent(distancePercentage);
            //KnotLinkCollection kk = spline.KnotLinkCollection;
            transform.position = currPos;

            //Vector3 nextPos = spline.EvaluatePosition(distancePercentage + 0.01f);
            //Vector3 dir = nextPos - currPos;

            // Set the rotation based on the selected axis
            switch (enemy.facingDir)
            {
                case 1:
                    transform.right = currTan;
                    break;
                case -1:
                    transform.right = -currTan;
                    break;
            }
        }
    }

    public void StartMove()
    {
        canStart = true;
    }
}