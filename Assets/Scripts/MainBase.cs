using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBase : MonoBehaviour, IDamageable
{
    [SerializeField] Health health;

    public static MainBase Instance;

    public event EventHandler OnBaseDestroyed;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        health.OnDie += Health_OnDie;
    }

    void Health_OnDie(object sender, EventArgs e)
    {   
        OnBaseDestroyed?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }

    public bool OwnedByPlayer()
    {
        return true;
    }
}
