using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth;

    int health;

    public event EventHandler OnHealthChanged;
    public event EventHandler OnDie;

    void Awake()
    {
        health = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        int newHealth = health - damage;
        ChangeHealth(newHealth);
    }

    public void Heal(int healAmt)
    {
        int newHealth = health + healAmt;
        ChangeHealth(newHealth);
    }


    void ChangeHealth(int newHealth)
    {
        health = Mathf.Clamp(newHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        if (health == 0)
        {   
            OnDie?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetCurrentHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

}
