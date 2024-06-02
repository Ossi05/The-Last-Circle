using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class SpawnParticlesOnDeath : MonoBehaviour
{
    [SerializeField] ParticleSystem deathParticles;
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
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
    }


}
