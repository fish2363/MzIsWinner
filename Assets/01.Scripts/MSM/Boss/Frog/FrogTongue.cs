using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FrogTongue : MonoBehaviour
{
    [SerializeField]
    private GameObject tongue;
    private Vector2 tongueDirect;
    private float angle;

    private Player player;

    private bool isTongue;

    [SerializeField]
    private SpriteRenderer tongueSprite;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }

    public void Attack()
    {
        tongueSprite.DOKill();
        tongueSprite.DOFade(0.7f, 1f);
        isTongue = true;
    }

    private void Update()
    {
        if(isTongue)
        {
            angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            tongue.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
