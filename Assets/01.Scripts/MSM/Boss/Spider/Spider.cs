using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour,IBoss
{
    [SerializeField] Transform targetTrans;
    private SpiderDown spiderDown;
    private SpiderSpit spiderSpit;
    private SpiderBall spiderBall;
    [SerializeField] private float coolTime;
    [SerializeField]Animator anim;
    [SerializeField] WeaknessPoint Weakness;
    [SerializeField] float weaknessTime;
    [SerializeField] float AttackDown;
    [SerializeField] float speed;
    [SerializeField] BoxCollider2D box;
    Rigidbody2D rb;
    bool isFirst = true;
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        spiderDown = GetComponentInChildren<SpiderDown>();
        spiderSpit = GetComponentInChildren<SpiderSpit>();
        spiderBall = GetComponentInChildren<SpiderBall>();
    }
    private void Start()
    {
        targetTrans = FindAnyObjectByType<Player>().transform;
        spiderDown.SetTarget(targetTrans);
        spiderDown.SetAnimator(anim);
        StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        int rand = Random.Range(0, 7);
        if (isFirst)
        {
            rand = Random.Range(0, 6);
            isFirst = false;
        }
        switch (rand)
        {
            case 0:
            case 1:
                yield return StartCoroutine(spiderDown.Attack());
                break;
            case 2:
            case 3:
                spiderSpit.Attack();
                AnimationPlayer.Instance.PlayAnimaiton(anim,"SpiderSpit");
                SoundManager.Instance.ChangeMainStageVolume("spiderSpit", true, ISOund.SFX);
                yield return new WaitForSeconds(0.3f);
                break;
            case 4:
            case 5:
                spiderBall.Attack();
                AnimationPlayer.Instance.PlayAnimaiton(anim, "SpiderSpit");
                SoundManager.Instance.ChangeMainStageVolume("spiderSpit", true, ISOund.SFX);
                yield return new WaitForSeconds(0.3f);
                break;
            default:
                yield return StartCoroutine(Weaknesss());
                break;
        }
        AnimationPlayer.Instance.PlayAnimaiton(anim, "SpiderIdle");
        yield return new WaitForSeconds(coolTime);
        StartCoroutine(Attack());
    }

    public IEnumerator Weaknesss()
    {
        box.enabled = false;
        Vector3 trans  = transform.position;
        while (transform.position.y > AttackDown)
        {
            rb.velocity = new Vector3(0, AttackDown - transform.position.y, 0).normalized * speed * 10;
            yield return null;
        }

        rb.velocity = Vector3.zero;

        transform.position = new Vector3(transform.position.x, AttackDown);

        Weakness.isRest = true;

        yield return new WaitForSeconds(weaknessTime);

        Weakness.isRest = false;
        box.enabled = true;
        while (transform.position.y < trans.y)
        {
            rb.velocity = new Vector3(0, trans.y - transform.position.y, 0).normalized * speed;
            yield return null;
        }

        rb.velocity = Vector3.zero;

        transform.position = new Vector3(transform.position.x, trans.y);
    }

    public void DeathBoss()
    {

    }
}
