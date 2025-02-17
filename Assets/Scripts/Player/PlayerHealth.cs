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
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        
    }
    public void Damage(float damageAmount)
    {
        health = Mathf.Max(0.0f, health - damageAmount);
    }
    public void Heal(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }
}
