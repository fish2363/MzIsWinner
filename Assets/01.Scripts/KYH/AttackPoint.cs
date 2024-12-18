using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackPoint : MonoBehaviour
{


    private Player player;
    private float angle;
    private SpriteRenderer attackPoint;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        attackPoint = GetComponentInChildren<SpriteRenderer>();
    }

    public void FadeInAttackPoint()
    {
        attackPoint.DOKill();
        attackPoint.DOFade(0.7f,1f);
    }

    public void FadeOutAttackPoint()
    {
        attackPoint.DOKill();
        attackPoint.DOFade(0, 0.2f);
    }

    void Update()
    {
        angle = Mathf.Atan2(player.GetWorldMousePosition().y - transform.position.y, player.GetWorldMousePosition().x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
