using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [field: SerializeField] public Health Health { get; private set; }

    public static Player Instance;

    public event EventHandler OnPlayerDeath;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Health.OnDie += Health_OnDie;
    }

    void Health_OnDie(object sender, System.EventArgs e)
    {   
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        Health.TakeDamage(damage);
    }

    public bool OwnedByPlayer()
    {
        return true;
    }
}
