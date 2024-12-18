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
            playerKeyAction.Player.SetCallbacks(this);  //플레이어 인풋이 발생하면 이 인스턴스를 연결
        }
        playerKeyAction.Player.Enable(); //활성화
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
        OnMoveEvent?.Invoke(moveDir);
    }

    public Vector3 GetWorldMousePosition()
    {
        mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);//스크린을 월드 좌표계로 바꾼다
        return mouseDir;
    }


}
