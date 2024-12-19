using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour,IBoss
{
    Transform targetTrans;
    public Animator frogAnimator;

    public ParticleSystem[] deathParticle;

    bool canAttack = true;
    public bool isTongueAttack { get; set; }
    [SerializeField] float attackCoolTime;
    float _timer;

    FrogSpit spit;

    [SerializeField] float attackPlusJumpCool;
    float JumpCoolTime;
    bool canJump;
    bool isRest;
    FrogJump jump;

    public BoxCollider2D frogFeet;

    FrogTongue tongue;

    [SerializeField]
    private WeaknessPoint weakPoint;

    public bool isDeath;

    private void Awake()
    {
        spit = GetComponentInChildren<FrogSpit>();
        jump = GetComponentInChildren<FrogJump>();
        tongue = GetComponentInChildren<FrogTongue>();
    }
    private void Start()
    {
        targetTrans = FindAnyObjectByType<Player>().transform;
        jump.TargetSet(targetTrans);
    }
    private void Update()
    {
        if(!isRest && !isDeath)
        {
            if (!canAttack && !isTongueAttack)
            {
                _timer += Time.deltaTime;
                if (_timer > attackCoolTime)
                {
                    canAttack = true;
                    _timer = 0;
                }
            }
            if (!canJump)
            {
                JumpCoolTime -= Time.deltaTime;
                if (JumpCoolTime < 0)
                {
                    canJump = true;
                }
            }
            if (canAttack && canJump)
            {
                Attack();
            }
            else if (canAttack)
            {
                NoJumpAttack();
            }
        }
    }

    private void NoJumpAttack()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                spit.Attack();
                break;
            case 1:
                tongue.Attack();
                isTongueAttack = true;
                break;
            case 2:
                Rest();
                break;
            default:
                break;
        }
        canAttack = false;
    }

    private void Attack()
    {
        int rand = Random.Range(0, 2);
        switch(rand)
        {
            case 0:
                spit.Attack();
                break;
            case 1:
                jump.Attack();
                JumpCoolTime = attackCoolTime + attackPlusJumpCool;
                canJump = false;
                break;
            default:
                break;
        }
        canAttack = false;
    }

    private void Rest()
    {
        isRest = true;
        AnimationPlayer.Instance.PlayAnimaiton(frogAnimator, "FrogRest");
        weakPoint.isRest = true;
        weakPoint.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.4f);
        weakPoint.GetComponent<BoxCollider2D>().enabled = true;
        isRest = false;
        weakPoint.isRest = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isRest) return;
        AnimationPlayer.Instance.PlayAnimaiton(frogAnimator, "FrogIdle");
        SoundManager.Instance.ChangeMainStageVolume("Landing",true,ISOund.SFX);
        frogFeet.enabled = false;

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
        ScreenShakeManager.Instance.ScreenShake(2f, true,100, true, 1f);
        for (int i =0; i<deathParticle.Length; i++)
        {
            deathParticle[i].Play();
        }
        GameManager.Instance.FadeIn();
        ScreenShakeManager.Instance.ScreenShake(0f, true, 100, true, 1f);
        yield return new WaitForSeconds(5f);
        Time.timeScale = 1f;
        print("°³±¸¸® µðÁü");
        GameManager.Instance.NextStage();
    }
}
