using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField] int maxDamage = 75;
    [SerializeField] int minDamage = 10;
    [SerializeField] float criticalHitChange = 40;
    [SerializeField] int criticalHitMultiplier = 2;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {   
            int damage = Random.Range(minDamage, maxDamage);
            bool isCritical = Random.Range(0, 100) < criticalHitChange;
            if (isCritical)
            {
                damage *= criticalHitMultiplier;
            }
            damageable.TakeDamage(damage);
            Vector2 hitPosition = collision.transform.position;
            DamagePopupCreator.Instance.CreateDamagePopup(hitPosition, damage, isCritical);
        }
    }
}
