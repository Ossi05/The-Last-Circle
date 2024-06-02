using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Health health;

    void Start()
    {
        health.OnHealthChanged += Health_OnHealthChanged;
        UpdateHealthBar();
    }

    private void Health_OnHealthChanged(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float fillAmount = health.GetCurrentHealth() / (float)health.GetMaxHealth();
        healthBar.fillAmount = fillAmount;
    }
}
