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
    bool isFirst = true;
    private void Awake()
    {
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
                Weakness.isRest = true;
                yield return new WaitForSeconds(weaknessTime);
                Weakness.isRest = false;
                break;
        }
        AnimationPlayer.Instance.PlayAnimaiton(anim, "SpiderIdle");
        yield return new WaitForSeconds(coolTime);
        StartCoroutine(Attack());
    }

    public void DeathBoss()
    {

    }
}
