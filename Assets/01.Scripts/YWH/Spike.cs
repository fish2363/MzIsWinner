using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Spike : MonoBehaviour
{
    private SpriteRenderer spikeSprite;
    private bool isHitting;

    private void Awake()
    {
        spikeSprite = GetComponent<SpriteRenderer>();
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            if (isHitting)
            {
                return;
            }
            isHitting = true;
            collision.GetComponent<Player>().Damage(1);
           

            transform.DOScale(new Vector3(transform.position.x * 0.5f, transform.position.y * 0.3f), 0.3f);
            spikeSprite.DOFade(0, 0.4f).OnComplete(() => { Destroy(gameObject); });
            ScreenShakeManager.Instance.ScreenShake(30, true, 0.3f, true, 0.2f);

            
        }            
    }
}
