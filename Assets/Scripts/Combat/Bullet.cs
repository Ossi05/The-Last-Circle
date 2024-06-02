using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] float lifeTime = 4f;
    Rigidbody2D rb;
    int damageAmt;
    bool criticalHit;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        StartCoroutine(DisableAfterTime());
    }

    IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    public void Shoot(Vector2 position, Quaternion rotation, int damage, bool isCriticalHit, float speed)
    {
        damageAmt = damage;
        criticalHit = isCriticalHit;
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
        AddVelocity(speed);
    }

   void AddVelocity(float speed)
    {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable iDamageable))
        {   
            if (!iDamageable.OwnedByPlayer())
            {
                iDamageable.TakeDamage(damageAmt);
                DamagePopupCreator.Instance.CreateDamagePopup(transform.position, damageAmt, criticalHit);
            }
            
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            

        }

        gameObject.SetActive(false);
    }

}
