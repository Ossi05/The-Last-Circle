using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseEnemy : MonoBehaviour
{
    public static event EventHandler OnAnyEnemyKilled;
    public static event EventHandler OnAnyEnemyDied;

    public static List<Transform> EnemiesAlive = new List<Transform>();

    [SerializeField] protected SpriteRenderer enemySprite;

    protected float distanceToTarget;
    protected Rigidbody2D rb;
    protected Health health;

    protected bool playerEngagedWithEnemy;
    protected Transform currentTarget;

    protected float cooldownTimer;
    protected Color normalColor;

    protected float movementSpeed;
    protected const string PLAYER_TAG = "Player";

    public static void ResetStaticData()
    {
        EnemiesAlive.Clear();
    }

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    public virtual void Start()
    {   
        
        normalColor = enemySprite.color;
    }

    public virtual void OnEnable()
    {
        EnemiesAlive.Add(transform);
    }

    protected float GetDistanceToTarget()
    {
        return Vector2.Distance(transform.position, currentTarget.position);
    }

    protected void LookAtTarget()
    {
        // Look at the target
        // Rotate only in the Z axis
        Vector2 direction = (currentTarget.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle - 90));
   
    }

    protected void SetTarget(EnemyTargetType target)
    {
        switch (target)
        {
            default:
            case EnemyTargetType.Player:
                currentTarget = Player.Instance.gameObject.transform;
                break;
            case EnemyTargetType.All:
            case EnemyTargetType.Base:
                currentTarget = MainBase.Instance.gameObject.transform;
                break;  
        }

    }

    protected void HandleEnemyKilled()
    {
        
        OnAnyEnemyKilled?.Invoke(this, EventArgs.Empty);
        OnAnyEnemyDied?.Invoke(this, EventArgs.Empty);
        EnemiesAlive.Remove(transform);
        Destroy(gameObject);
    }

    protected void HandleEnemyDied()
    {   
        OnAnyEnemyDied?.Invoke(this, EventArgs.Empty);
        EnemiesAlive.Remove(transform);
        Destroy(gameObject);
    }
}
