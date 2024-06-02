using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fast Enemy", menuName = "Enemies/New Fast Enemy")]
public class FastEnemySO : BaseEnemySO
{
    [Header("Fast enemy")]
    public bool FollowTargetWhileAttacking;
    public float AttackDuration;
    public ParticleSystem PlayerHitParticles;
}
