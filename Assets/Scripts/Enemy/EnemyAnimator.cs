using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    const string ATTACK_TRIGGER = "Attack";


    public void PlayAttackAnimation()
    {
        animator.SetTrigger(ATTACK_TRIGGER);
    }
}
