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
    [SerializeField]
    private SpriteRenderer tongueWarning;

    private Animator tongueAnim;

    private float tongueTime;
    private float tongueMaxTime = 3f;


    private void Awake()
    {
        tongueAnim = tongueSprite.GetComponent<Animator>();
    }

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    public void Attack()
    {
        tongueSprite.DOKill();
        tongueSprite.DOFade(1f, 1f);
        tongueWarning.DOFade(0.5f, 1f);
        isTongue = true;
        AnimationPlayer.Instance.PlayAnimaiton(frog.frogAnimator, "FrogMouseOpen");
    }

    private void Update()
    {
        if (isTongue)
        {
            angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            tongue.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            tongueTime += Time.deltaTime;
            if (tongueMaxTime < tongueTime)
            {
                isTongue = false;
                AnimationPlayer.Instance.PlayAnimaiton(tongueAnim, "Tongue");
                tongue.GetComponentInChildren<ParticleSystem>().Play();
                SoundManager.Instance.ChangeMainStageVolume("tongue", true, ISOund.SFX);
                tongueWarning.DOFade(0f, 1f);
                StartCoroutine(Routine());
            }
        }
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSeconds(0.3f);
        tongueAnim.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.7f);
        isTongue = false;
        frog.isTongueAttack = false;
        tongueTime = 0f;
        tongueAnim.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        AnimationPlayer.Instance.PlayAnimaiton(tongueAnim, "TongueOut");
    }
}
