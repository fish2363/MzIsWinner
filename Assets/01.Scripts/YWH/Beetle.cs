using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Beetle : MonoBehaviour
{
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 50  , ForceMode2D.Force);
            collision.GetComponent<Player>().Damage(1);

            transform.DOScale(originalScale * 1.2f, 0.1f).OnComplete(() => transform.DOScale(originalScale, 0.1f).SetDelay(0.3f));
            transform.DOShakeRotation(0.3f, 1, 10, 90); 
            ScreenShakeManager.Instance.ScreenShake(30, true, 0.3f, true, 0.2f);

            
        }
    }
}
