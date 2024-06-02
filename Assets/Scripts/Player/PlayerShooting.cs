
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    [Header("References")]
    [SerializeField] Transform spawnPoint;

    [Header("Settings")]
    [SerializeField] float projectileSpeed = 35f;
    [SerializeField] float fireRate = 0.75f;
    [SerializeField] int minDamage = 20;
    [SerializeField] int maxDamage = 100;
    //[SerializeField] float distance = 100f;
    //[SerializeField] LayerMask hitLayers;
    [Space]
    [SerializeField] float criticalHitChancePercentage = 30;
    [SerializeField] int criticalHitMultiplier = 2;


    bool firing;

    float previousFireTime;

    void Start()
    {
        Controls.Instance.OnFireAction += Controls_OnFireAction;
    }

    private void Update()
    {   
        if (!GameManager.Instance.IsGamePlaying()) { return; }

        if (previousFireTime > 0f)
        {
            previousFireTime -= Time.deltaTime;
        }

        if (!firing) { return; }

        if (previousFireTime <= 0)
        {
            previousFireTime = fireRate;
            Shoot();     
        }

        
    }

    void Shoot()
    {
        bool criticalHit = Random.Range(0, 100) < criticalHitChancePercentage;
        int damageAmt = Random.Range(minDamage, maxDamage);
        if (criticalHit)
        {
            damageAmt *= criticalHitMultiplier;
        }

        AudioManager.Instance.PlayShootingSound();
        Bullet bullet = BulletPool.Instance.GetAvailableBullet();
        bullet.Shoot(spawnPoint.position, spawnPoint.rotation, damageAmt, criticalHit, projectileSpeed);
    }

    private void Controls_OnFireAction(bool firing)
    {
        this.firing = firing;
    }
}
