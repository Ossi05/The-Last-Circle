using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour {
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] int bulletsToCreate = 50;

    List<Bullet> bulletPool;

    public static BulletPool Instance;

    private void Awake()
    {
        Instance = this;

        bulletPool = new List<Bullet>();

        for (int i = 0; i < bulletsToCreate; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab.gameObject, transform).GetComponent<Bullet>();
            bullet.gameObject.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public Bullet GetAvailableBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            Bullet bullet = bulletPool[i];

            if (!bullet.gameObject.activeSelf)
            {
                bulletPool.RemoveAt(i);
                bulletPool.Add(bullet);

                return bullet;
            }
        }

        return bulletPool[0];
    }
}
