using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : BaseEnemy, IDamageable
{   
    [SerializeField] FastEnemySO enemyData;

    float attackDurationTimer;
    state currentState;

    public override void Start()
    {
        base.Start();
        SetTarget(enemyData.Target);
        health.OnHealthChanged += Health_OnHealthChanged;
        health.OnDie += Health_OnDie;
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
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {   
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {   
                Instantiate(enemyData.PlayerHitParticles, collision.GetContact(0).point, Quaternion.identity, collision.transform);
                damageable.TakeDamage(enemyData.DamageAmt);
                HandleEnemyDied();
            }
        }
    }

    enum state
    {
        Normal,
        Attacking,
        Recharging
    }

    void Update()
    {
        distanceToTarget = GetDistanceToTarget();

        switch (currentState)
        {
            case state.Normal:
                movementSpeed = enemyData.MovementSpeed;
                enemySprite.color = normalColor;
                LookAtTarget();
                if (distanceToTarget <= enemyData.AttackRange)
                {
                    attackDurationTimer = enemyData.AttackDuration;
                    currentState = state.Attacking;
                }
                break;
            case state.Attacking:
                movementSpeed = enemyData.AttackMovementSpeed;
                enemySprite.color = enemyData.AttackingColor;

                if (enemyData.FollowTargetWhileAttacking)
                {
                    LookAtTarget();
                }

                attackDurationTimer -= Time.deltaTime;

                if (attackDurationTimer <= 0)
                {

                    cooldownTimer = enemyData.AttackCooldownTime;
                    currentState = state.Recharging;
                }
                break;
            case state.Recharging:
                movementSpeed = enemyData.CooldownMovementSpeed;
                enemySprite.color = enemyData.CooldownColor;

                LookAtTarget();

                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0)
                {
                    currentState = state.Normal;
                }
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case state.Recharging:
            case state.Normal:
                rb.velocity = (currentTarget.position - transform.position).normalized * movementSpeed; // Move towards the target
                break;
            case state.Attacking:
                if (enemyData.FollowTargetWhileAttacking)
                {   
                    // Move towards the player
                    rb.velocity = (currentTarget.position - transform.position).normalized * movementSpeed;
                }
                else
                {   
                    // Move forward
                    rb.velocity = transform.up * movementSpeed;
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
