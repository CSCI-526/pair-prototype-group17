using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    public GameObject target;
    public bool isFunctioning;
    private Rigidbody2D rb;
    public float lifeTime;
    public float lifeTimer = 0f;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isFunctioning = true;
        lifeTimer = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (isFunctioning)
        {
            spriteRenderer.color = Color.red;
            rb.velocity = transform.right * moveSpeed;

            float crossProductTest = Vector3.Cross(transform.right, (target.transform.position - transform.position)).z;
            // I did not normalized self to target vector since I want missile to be less accurrate when it is close to player, and avoid sqrt
            if (crossProductTest > 0.2f)
            {
                rb.angularVelocity = turnSpeed;
            }
            else if (crossProductTest < -0.2f)
            {
                rb.angularVelocity = -turnSpeed;
            }
            else
            {
                rb.angularVelocity = 0;
            }
        }
        else
        {
            spriteRenderer.color = Color.gray;
        }
        if (lifeTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    public void DisableMovement()
    {
        isFunctioning = false;
        //Destroy(gameObject, 1f);
    }
}
