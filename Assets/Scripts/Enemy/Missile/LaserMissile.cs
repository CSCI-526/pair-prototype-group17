using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMissile : InteractableProjectile
{
    public float moveSpeed;
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
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (isFunctioning)
        {
            spriteRenderer.color = Color.red;
            rb.velocity = transform.right * moveSpeed;
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

    public override void DisableMovement()
    {
        isFunctioning = false;
        rb.gravityScale = 1;
    }
}
