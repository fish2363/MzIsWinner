using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    [SerializeField]Transform targetTrans;
    BearHead head;
    BearStamp stamp;
    BearScratch scratch;
    bool first = true;
    private void Awake()
    {
        head = GetComponentInChildren<BearHead>();
        stamp = GetComponentInChildren<BearStamp>();
        scratch = GetComponentInChildren<BearScratch>();
    }
    private void Start()
    {
        targetTrans = FindAnyObjectByType<Player>().transform;
        stamp.SetTarget(targetTrans);
        scratch.TargetSet(targetTrans);
        StartCoroutine(BearStart());
    }
    private IEnumerator BearStart()
    {
        yield return StartCoroutine(head.HeadSee());
        StartCoroutine(Attack());
    }
    private IEnumerator BearHead()
    {
        if(first)
        {
            yield return StartCoroutine(head.HeadSee());
            first = false;
        }
        else
        {
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0:
                    yield return StartCoroutine(head.Weakness());
                    break;
                default:
                    yield return StartCoroutine(head.HeadSee());
                    break;
            }
        }
        StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        int rand = Random.Range(0, 2);
        switch (rand)
        {
            case 0:
                yield return StartCoroutine(stamp.Attack());
                break;
            case 1:
                yield return StartCoroutine(scratch.Attack());
                break;
            default:
                break;
        }
        StartCoroutine(BearHead());
    }
}
