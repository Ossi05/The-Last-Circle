using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    public static Controls Instance;

    void Awake()
    {
        Instance = this;
        playerInput = GetComponent<PlayerInput>();
    }

   
    public event Action<bool> OnFireAction;
    public event Action OnGamePaused;
    public event Action OnShockwaveAction;

    PlayerInput playerInput;

    public Vector2 PreviousMoveInput { get; private set; }
    public Vector2 mouseScreenPosition { get; private set; }
    

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseScreenPosition = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        PreviousMoveInput = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnFireAction?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnFireAction?.Invoke(false);
        }
    }

    public void OnShockwave(InputAction.CallbackContext context)
    {
        if (context.performed)
        {   
            OnShockwaveAction?.Invoke();
        }
    }

    public void OnPaused(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnGamePaused?.Invoke();
        }
    }

    public string GetCurrentControlScheme()
    {
        return playerInput.currentControlScheme;
    }

}
