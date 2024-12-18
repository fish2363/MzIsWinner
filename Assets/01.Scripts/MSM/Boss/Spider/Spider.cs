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
        spiderDown.SetTarget(targetTrans);
        spiderDown.SetAnimator(anim);
        StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                yield return StartCoroutine(spiderDown.Attack());
                break;
            case 1:
                spiderSpit.Attack();
                break;
            case 2:
                spiderBall.Attack();
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(coolTime);
        StartCoroutine(Attack());
    }
}
