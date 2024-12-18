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
    private Frog frog;

    [SerializeField]
    private SpriteRenderer tongueSprite;

    private Animator tongueAnim;

    private float tongueTime;
    private float tongueMaxTime = 3f;


    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        tongueAnim = tongueSprite.GetComponent<Animator>();
    }

    public void Attack()
    {
        tongueSprite.DOKill();
        tongueSprite.DOFade(1f, 1f);
        isTongue = true;
    }

    private void Update()
    {
        if(isTongue)
        {
            angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            tongue.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            tongueTime += Time.deltaTime;
        }
        if(tongueMaxTime < tongueTime)
        {
            isTongue = false;
            AnimationPlayer.Instance.PlayAnimaiton(tongueAnim, "Tongue");
            StartCoroutine(Routine());
        }
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSeconds(1f);
        isTongue = false;
        frog.isTongueAttack = false;
        tongueTime = 0f;
        AnimationPlayer.Instance.PlayAnimaiton(tongueAnim, "TongueOut");
    }
}
