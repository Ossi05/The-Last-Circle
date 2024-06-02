using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform bodyTransform;
    [SerializeField] Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField] float movementSpeed = 4f;

    bool canMove;

    void Update()
    {       
        canMove = GameManager.Instance.IsGamePlaying();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }


    void MovePlayer()
    {   
        if (canMove)
        {
            rb.velocity = Controls.Instance.PreviousMoveInput * movementSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
    }

}
