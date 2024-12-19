using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    [SerializeField]Transform targetTrans;
    BearHead head;
    BearStamp stamp;
    BearScratch scratch;
    private void Awake()
    {
        head = GetComponentInChildren<BearHead>();
        stamp = GetComponentInChildren<BearStamp>();
        scratch = GetComponentInChildren<BearScratch>();
    }
    private void Start()
    {
        stamp.SetTarget(targetTrans);
        scratch.TargetSet(targetTrans);
        StartCoroutine(BearHead());
    }
    private IEnumerator BearHead()
    {
        yield return StartCoroutine(head.HeadSee());
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
