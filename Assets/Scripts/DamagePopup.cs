using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{

    [SerializeField] TextMeshPro damageText;
    [SerializeField] int criticalHitFontSizeIncrease = 2;
    [SerializeField] Color criticalDamageColor = Color.red;

    public void Setup(int damageAmt, bool criticalHit)
    {
        if (criticalHit)
        {   
            damageText.fontSize = damageText.fontSize + criticalHitFontSizeIncrease;
            damageText.color = criticalDamageColor;
        }

        damageText.text = damageAmt.ToString();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
