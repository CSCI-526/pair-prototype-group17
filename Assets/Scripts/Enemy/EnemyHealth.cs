using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthBar;
    public float health;
    public float maxHealth = 100.0f;
    public Enemy enemy;
    public Vector3 offset = new Vector3(0.0f, 2.0f, 0.0f); //
    
    void Start()
    {
        enemy = GetComponent<Enemy>();
        //healthBar.rectTransform.anchoredPosition = offset + enemy.transform.position;
        health = maxHealth;
        //enemy = GetComponent<Enemy>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        //healthBar.rectTransform.anchoredPosition = offset+enemy.transform.position;
        
    }
    public void Damage(float damageAmount)
    {
        health = Mathf.Max(0.0f, health - damageAmount);
    }
    public void Heal(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }
    void UpdateHealthBar()
    {
        health = enemy.health;
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        //ChangeHealthBarColor();
    }

    //Not sure if this is needed for enemy as well, so I comment it out for now
    //void ChangeHealthBarColor()
    //{
    //    if (health <= maxHealth * 0.25f) // 25% or less
    //    {
    //        healthBar.color = Color.red;
    //    }
    //    else if (health <= maxHealth * 0.50f) // Between 25% and 50%
    //    {
    //        healthBar.color = Color.yellow;
    //    }
    //    else // More than 50%
    //    {
    //        healthBar.color = new Color(0, 1, 0); // Pure green
    //    }
    //}
}
