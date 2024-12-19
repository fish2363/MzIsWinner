using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private List<Transform> childTransforms;
    private bool isOpen;
    private void Awake()
    {
       
        childTransforms = new List<Transform>();
        foreach (Transform child in transform)
        {
            childTransforms.Add(child);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !isOpen)
        {
            isOpen = true;
            foreach (Transform child in childTransforms)
            {
                ScreenShakeManager.Instance.ScreenShake(20, true, 0.2f, true, 0.2f);
                child.DOScaleY(0, 10f).SetEase(Ease.Linear).SetDelay(2); 
            }
        }
    }
}
