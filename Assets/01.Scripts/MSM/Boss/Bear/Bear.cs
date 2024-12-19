using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour,IBoss
{
    [SerializeField]Transform targetTrans;
    public ParticleSystem[] deathParticle;

    BearHead head;
    BearStamp stamp;
    BearScratch scratch;
    public bool isDeath;
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

    public void DeathBoss()
    {
        isDeath = true;
        StartCoroutine(DeadRoutine());
    }
    private IEnumerator DeadRoutine()
    {
        ScreenShakeManager.Instance.ScreenShake(5f, true, 5, true, 5f);
        yield return new WaitForSeconds(3f);
        ScreenShakeManager.Instance.ScreenShake(50f, true, 100, true, 2f);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0.2f;
        ScreenShakeManager.Instance.ScreenShake(2f, true, 100, true, 1f);
        for (int i = 0; i < deathParticle.Length; i++)
        {
            deathParticle[i].Play();
        }
        GameManager.Instance.FadeIn();
        ScreenShakeManager.Instance.ScreenShake(0f, true, 100, true, 1f);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1f;
        GameManager.Instance.NextStage();
    }
}
