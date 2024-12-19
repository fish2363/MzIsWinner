using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class ChatManager : MonoBehaviour
{
    public static ChatManager Instance;

    private float putInTimer;
    private bool isPutInTimer;
    private float textDelayCounter;

    [Header("��� �ؽ�Ʈ CoreCanvas=>TextMessage")]
    [SerializeField]
    private TextMeshProUGUI _text;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //TextMessage("1��° �ؽ�Ʈ", 3f,Color.red, TextStyle.UI | TextStyle.FadeIn)
        //    .TextMessage("2��° �ؽ�Ʈ", 1f, Color.blue, TextStyle.UI | TextStyle.FadeIn)
        //    .TextMessage("3��° �ؽ�Ʈ", 1f, Color.green, TextStyle.UI | TextStyle.FadeIn)
        //    .End();
    }

    public ChatManager TextMessage(string text, float second,Color color,TextStyle textStyle = default)
    {
        SetText(text, second,color,textStyle);
        putInTimer = second;
        isPutInTimer = true;
        return this;
    }

    private void SetText(string text, float second, Color color, TextStyle textStyle = default)
    {
        DOVirtual.DelayedCall(textDelayCounter, () =>
        {
            _text.SetText(text);               //�ؽ�Ʈ�� �Է�
            _text.TextMove(second, color, textStyle);
            _text.ForceMeshUpdate();
        });
        textDelayCounter += second;
    }

    
    public void End()
    {
        DOVirtual.DelayedCall(textDelayCounter, () =>
        {
            Hide();
        });
        textDelayCounter = 0;
    }

    private void Hide()
    {
        isPutInTimer = false;
        return;
    }
}
