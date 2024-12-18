using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, KeyAction.IPlayerActions
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnAttackEvent;
    public event Action OnDashEvent;
    public event Action<bool> OnAttackingEvent;
    public Vector3 direction { get; private set; }
    public Vector2 moveDir { get; private set; }

    private KeyAction playerKeyAction;

    private void OnEnable()
    {
        if (playerKeyAction == null)
        {
            playerKeyAction = new KeyAction();
            playerKeyAction.Player.SetCallbacks(this);
        }
        playerKeyAction.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
        direction = new Vector2(moveDir.x, moveDir.y);

        moveDir.Normalize();
        OnMoveEvent?.Invoke(moveDir);
    }

    

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnAttackingEvent?.Invoke(true);
        }
        if(context.canceled)
        {
            OnAttackEvent?.Invoke();
            OnAttackingEvent?.Invoke(false);
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnDashEvent?.Invoke();
        }
    }
}
