using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : BaseEnemy, IDamageable {

    [SerializeField] TankEnemySO enemyData;
    state currentState;

    bool hitTarget;
    public override void Start()
    {
        base.Start();
        health.OnHealthChanged += Health_OnHealthChanged;
        health.OnDie += Health_OnDie;
        SetTarget(enemyData.Target);
        movementSpeed = enemyData.MovementSpeed;
        enemySprite.color = normalColor;
        currentState = state.Normal;
    }

    void Health_OnHealthChanged(object sender, EventArgs e)
    {

    }

    void Health_OnDie(object sender, EventArgs e)
    {
        HandleEnemyKilled();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == currentTarget.gameObject)
        {
            // We hit our target
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(enemyData.DamageAmt);
            }
            hitTarget = true;
        }
    }

    enum state
    {
        Normal,
        Attacking,
        BackingUp,
        Cooldown,
    }

    private void Update()
    {
        distanceToTarget = GetDistanceToTarget();
        LookAtTarget();

        switch (currentState)
        {
            case state.Normal:

                if (distanceToTarget <= enemyData.AttackRange)
                {
                    movementSpeed = enemyData.AttackMovementSpeed;
                    enemySprite.color = enemyData.AttackingColor;
                    currentState = state.Attacking;
                }

                break;
            case state.Attacking:

                if (hitTarget)
                {
                    hitTarget = false;
                    movementSpeed = enemyData.BackingupSpeed;
                    enemySprite.color = enemyData.CooldownColor;
                    currentState = state.BackingUp;
                }
                break;
            case state.BackingUp:          
                break;
            case state.Cooldown:
                
                cooldownTimer -= Time.deltaTime;

                if (cooldownTimer <= 0)
                {
                    enemySprite.color = normalColor;
                    movementSpeed = enemyData.MovementSpeed;
                    currentState = state.Normal;
                }

                break;
        }
    }

    void FixedUpdate()
    {
        switch (currentState)
        {  
            case state.Attacking:
            case state.Normal:
                rb.velocity = (currentTarget.position - transform.position).normalized * movementSpeed; // Move towards the target
                break;
            case state.BackingUp:
                // Move backwards
                rb.velocity = (transform.position - currentTarget.position).normalized * movementSpeed;

                if (Vector2.Distance(transform.position, currentTarget.position) >= enemyData.BackupDistance)
                {
                    cooldownTimer = enemyData.AttackCooldownTime;
                    movementSpeed = enemyData.CooldownMovementSpeed;
                    currentState = state.Cooldown;
                }
                break;       
            default:
                    break;
        }
    }


    public void TakeDamage(int damage)
    {
        playerEngagedWithEnemy = true;
        health.TakeDamage(damage);
    }

    public bool OwnedByPlayer()
    {
        return false;
    }

    void OnDestroy()
    {
        health.OnHealthChanged -= Health_OnHealthChanged;
        health.OnDie -= Health_OnDie;
    }
}
