using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveCharger : MonoBehaviour
{
    float speed = 4f;

    void Update()
    {
        Vector3 targetPosition = MainBase.Instance.gameObject.transform.position;

        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            ShockwaveSpawner.Instance.Charge();
            Destroy(gameObject);
        }
    }
}
