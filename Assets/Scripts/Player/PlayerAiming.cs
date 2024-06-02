using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] Transform aimTransform;
    [SerializeField] Transform weapon;


    void Update()
    {
        Vector2 mouseScreenPosition = Controls.Instance.mouseScreenPosition;
        Vector2 aimWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        aimTransform.up = new Vector2(
            aimWorldPosition.x - aimTransform.position.x,
            aimWorldPosition.y - aimTransform.position.y);
    }
}
