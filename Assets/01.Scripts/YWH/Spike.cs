using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Spike : MonoBehaviour
{
    private SpriteRenderer spikeSprite;

    private void Awake()
    {
        spikeSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.DOScale(new Vector3(transform.position.x * 0.5f, transform.position.y * 0.5f), 0.5f);
        spikeSprite.DOFade(0, 0.4f);
        ScreenShakeManager.Instance.ScreenShake(30, true, 0.5f, true, 0.2f);    
    }
}
