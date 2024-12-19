using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeetleTrigger : MonoBehaviour
{
    [SerializeField] private Transform beetle;
    [SerializeField] private Transform movepos;
    [SerializeField] private Transform originpos;

    private bool isAttaking;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (isAttaking)
            {
                return;
            }

            isAttaking = true;

            beetle.DOMoveY(movepos.position.y, 0.5f).SetEase(Ease.InSine).OnComplete(() =>
            { beetle.DOMoveY(originpos.position.y, 0.5f).SetEase(Ease.OutSine).SetDelay(2).OnComplete(() => { isAttaking = false; }); });
        }
        
   }
}
