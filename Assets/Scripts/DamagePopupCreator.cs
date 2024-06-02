using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupCreator : MonoBehaviour
{
    [SerializeField] GameObject damagePopupPrefab;

    public static DamagePopupCreator Instance;

    void Awake()
    {
        Instance = this;
    }

    public void CreateDamagePopup(Vector2 position, int damage, bool criticalHit)
    {
        GameObject instance = Instantiate(damagePopupPrefab.gameObject, position, Quaternion.identity);
        DamagePopup damagePopup = instance.GetComponent<DamagePopup>();
        damagePopup.Setup(damage, criticalHit);
    }
}
