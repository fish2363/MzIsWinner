using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class BeetleTrigger : MonoBehaviour
{
    [SerializeField] private Transform beetle;
    [SerializeField] private Transform movepos;
    [SerializeField] private Transform originpos;
    [SerializeField] private SpriteRenderer mark;

    public float markSpeed = 0.12f;
    public float speed = 0.35f;

    private bool isAttacking = false; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (isAttacking) return;

            isAttacking = true;
            Light2D light = mark.GetComponent<Light2D>();
            DOTween.To(() => light.intensity, x => light.intensity = x, 1, markSpeed);
            mark.DOFade(1, markSpeed).OnComplete(() =>
            {
                mark.DOFade(0, markSpeed);
                DOTween.To(() => light.intensity, x => light.intensity = x, 0, markSpeed);
                beetle.DOMoveY(movepos.position.y, speed).SetDelay(0.15f)
                    .SetEase(Ease.InSine)
                    .OnComplete(() =>
                    {

                        beetle.DOMoveY(originpos.position.y, speed)
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
