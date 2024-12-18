using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : ScriptableObject, KeyAction.IPlayerActions
{
    public event Action<Vector2> OnMoveEvent;
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
        OnMoveEvent?.Invoke(moveDir);
    }

    public Vector3 GetWorldMousePosition()
    {
        mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);//��ũ���� ���� ��ǥ��� �ٲ۴�
        return mouseDir;
    }


}
