using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    [SerializeField]Transform targetTrans;
    BearHead head;
    BearStamp stamp;
    private void Awake()
    {
        head = GetComponentInChildren<BearHead>();
        stamp = GetComponentInChildren<BearStamp>();
    }
    private void Start()
    {
        stamp.SetTarget(targetTrans);
        StartCoroutine(BearHead());
    }
    private IEnumerator BearHead()
    {
        yield return StartCoroutine(head.HeadSee());
        StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        //int rand = Random.Range(0,2);
        //switch(rand)
        //{
        //    case 0:
        //        yield return StartCoroutine(stamp.Attack()); 
        //        break;
        //    case 1:
        //        yield return StartCoroutine(scratch.Attack());
        //        break;
        //    default: 
        //        break;
        //}
        yield return StartCoroutine(stamp.Attack());
        StartCoroutine(BearHead());
    }
}
