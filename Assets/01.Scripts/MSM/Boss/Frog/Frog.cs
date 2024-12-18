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
    FrogJump jump;

    [SerializeField]
    private Transform groundChecker;

    FrogTongue tongue;
    private LayerMask whatIsGround;
    private Vector2 checkerSize;
    private bool isGround;

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
        Collider2D hit = Physics2D.OverlapBox(transform.position, checkerSize, whatIsGround);
        if (hit != null)
        {
            AnimationPlayer.Instance.PlayAnimaiton(frogAnimator, "FrogMouseOpen");
        }
        if (!canAttack && !isTongueAttack)
        {
            _timer += Time.deltaTime;
            if(_timer > attackCoolTime)
            {
                canAttack = true;
                _timer = 0;
            }
        }
        if(!canJump)
        {
            JumpCoolTime -= Time.deltaTime;
            if(JumpCoolTime < 0)
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

    private void NoJumpAttack()
    {
        int rand = Random.Range(0, 2);
        switch (rand)
        {
            case 0:
                spit.Attack();
                break;
            case 1:
                tongue.Attack();
                isTongueAttack = true;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, checkerSize);
        Gizmos.color = Color.white;
    }
}
