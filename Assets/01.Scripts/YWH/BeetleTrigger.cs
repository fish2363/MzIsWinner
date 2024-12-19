using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeetleTrigger : MonoBehaviour
{
    [SerializeField] private Transform beetle;
    [SerializeField] private Transform movepos;
    [SerializeField] private Transform originpos;
    [SerializeField] private SpriteRenderer mark;

    private bool isAttacking = false; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (isAttacking) return;

            isAttacking = true;
            mark.DOFade(1, 0.2f).OnComplete(() =>
            {
                mark.DOFade(0, 0.1f);
                beetle.DOMoveY(movepos.position.y, 0.35f).SetDelay(0.15f)
                    .SetEase(Ease.InSine)
                    .OnComplete(() =>
                    {

                        beetle.DOMoveY(originpos.position.y, 0.5f)
                            .SetEase(Ease.OutSine)
                            .SetDelay(1.5f)
                            .OnComplete(() =>
                            {
                                isAttacking = false;
                            });
                    });
            });
        }
    }
}
