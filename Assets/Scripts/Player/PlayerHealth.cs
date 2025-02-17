using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthBar;
    public float health;
    public float maxHealth = 100.0f;
    void Start()
    {
        health = maxHealth;
        UpdateHealthBar();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();

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
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        ChangeHealthBarColor();
    }

    void ChangeHealthBarColor()
    {
        if (health <= maxHealth * 0.25f) // 25% or less
        {
            healthBar.color = Color.red;
        }
        else if (health <= maxHealth * 0.50f) // Between 25% and 50%
        {
            healthBar.color = Color.yellow;
        }
        else // More than 50%
        {
            healthBar.color = new Color(0, 1, 0); // Pure green
        }
    }
}
