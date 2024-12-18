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
    public Vector2 moveDir { get; private set; }
    public Vector2 mouseDir { get; private set; }

    private KeyAction playerKeyAction;

    private void OnEnable()
    {
        if (playerKeyAction == null)
        {
            playerKeyAction = new KeyAction();
            playerKeyAction.Player.SetCallbacks(this);  //�÷��̾� ��ǲ�� �߻��ϸ� �� �ν��Ͻ��� ����
        }
        playerKeyAction.Player.Enable(); //Ȱ��ȭ
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
        moveDir.Normalize();
        OnMoveEvent?.Invoke(moveDir);
    }

    public Vector3 GetWorldMousePosition()
    {
        mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);//��ũ���� ���� ��ǥ��� �ٲ۴�
        return mouseDir;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnAttackEvent?.Invoke();
        }
    }
}
