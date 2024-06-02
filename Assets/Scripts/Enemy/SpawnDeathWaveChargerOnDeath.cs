using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDeathWaveChargerOnDeath : MonoBehaviour
{
    [SerializeField] GameObject deathWaveCharger;
    Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Start()
    {
        health.OnDie += Health_OnDie;
    }

    void Health_OnDie(object sender, EventArgs e)
    {
        Instantiate(deathWaveCharger, transform.position, Quaternion.identity);
    }
}
