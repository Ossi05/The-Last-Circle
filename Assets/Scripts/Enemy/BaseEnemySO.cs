using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Base Enemy", menuName = "Enemies/New Base Enemy")]
public class BaseEnemySO : ScriptableObject
{   
    public EnemyType EnemyType;
    public EnemyTargetType Target;

    public float MovementSpeed; 
    [Header("Attack")]
    public int DamageAmt;
    public float AttackMovementSpeed;
    public float AttackRange;
    public float AttackCooldownTime;
    public Color AttackingColor = Color.red;
    [Header("Cooldown")]
    public float CooldownMovementSpeed;
    public Color CooldownColor = Color.green;

}
