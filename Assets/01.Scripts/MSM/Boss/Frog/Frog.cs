using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField]Transform targetTrans;
    public Animator frogAnimator;

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


    private void Awake()
    {
        spit = GetComponentInChildren<FrogSpit>();
        jump = GetComponentInChildren<FrogJump>();
        tongue = GetComponentInChildren<FrogTongue>();
    }
    private void Start()
    {
        jump.TargetSet(targetTrans);
    }
    private void Update()
    {
        if(!isRest)
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
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.3f);
        isRest = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isRest) return;
        AnimationPlayer.Instance.PlayAnimaiton(frogAnimator, "FrogIdle");
        frogFeet.enabled = false;
    }
}
