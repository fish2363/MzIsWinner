using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] Transform targetTrans;
    private SpiderDown spiderDown;
    private SpiderSpit spiderSpit;
    private SpiderBall spiderBall;
    [SerializeField] private float coolTime;
    [SerializeField]Animator anim;
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
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                yield return StartCoroutine(spiderDown.Attack());
                break;
            case 1:
                spiderSpit.Attack();
                AnimationPlayer.Instance.PlayAnimaiton(anim,"SpiderSpit");
                SoundManager.Instance.ChangeMainStageVolume("spiderSpit", true, ISOund.SFX);
                yield return new WaitForSeconds(0.3f);
                break;
            case 2:
                spiderBall.Attack();
                AnimationPlayer.Instance.PlayAnimaiton(anim, "SpiderSpit");
                SoundManager.Instance.ChangeMainStageVolume("spiderSpit", true, ISOund.SFX);
                yield return new WaitForSeconds(0.3f);
                break;
            default:
                //취약상태
                yield return new WaitForSeconds(Random.Range(1.0f,2.1f));
                break;
        }
        AnimationPlayer.Instance.PlayAnimaiton(anim, "SpiderIdle");
        yield return new WaitForSeconds(coolTime);
        StartCoroutine(Attack());
    }
}
