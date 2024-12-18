using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateEnum
{
    Idle,
    Move,
    Attack,
    Dash
}

public enum AnimationType
{
    PlayerIdle,
    PlayerAttackReady,
    PlayerMove,
    PlayerAttacking,
}

public interface IDamage
{
    void Damage(int damage);
}


public class Player : MonoBehaviour,IDamage
{
    [field: SerializeField] public InputReader inputReader { get; private set; }

    [Header("이동속도")]
    public float moveSpeed;

    [field: SerializeField] public Animator PlayerAnimator { get; private set; }
    public Rigidbody2D RigidCompo { get; private set; }


    [SerializeField]
    private Animator[] animators;
    public Animator AnimatorCompo { get; private set; }


    private Dictionary<StateEnum, State> stateDictionary = new Dictionary<StateEnum, State>();
    private StateEnum currentEnum;

    [Header("공격 딜레이")]
    [SerializeField]
    private float attackDelay;

    [field: SerializeField] public float DashPower { get; private set; }

    [HideInInspector]
    public AttackPoint attackPoint;

    public Vector2 mouseDir { get; private set; }

    public bool isStopMove { get; set; }
    public bool isAttack { get; set; }

    public CharacterSO currentChracter;

    public LayerMask whatIsEntity;

    [Header("최대 체력")]
    public int MaxHp = 3;
    [Header("현재 체력")]
    public int CurrentHp;
    [HideInInspector]
    public float checkerRadius =1f;
    public bool isUndead { get; set; }

    private void Awake()
    {
        RigidCompo = GetComponent<Rigidbody2D>();
        attackPoint = GetComponentInChildren<AttackPoint>();

        foreach (StateEnum enumState in Enum.GetValues(typeof(StateEnum)))
        {
            Type t = Type.GetType($"{enumState}State");
            State state = Activator.CreateInstance(t, new object[] { this }) as State;
            stateDictionary.Add(enumState, state);
        }

        inputReader.OnAttackEvent += HandleAttackEvent;
        inputReader.OnAttackingEvent += HandleAttackingEvent;
        inputReader.OnDashEvent += HandleDashEvent;
    }

    private void Start()
    {
        moveSpeed = currentChracter.moveSpeed;
        MaxHp = currentChracter.maxHp;
        CurrentHp= MaxHp;
        AnimatorCompo = animators[currentChracter.beeIdx];
        AnimatorCompo.gameObject.SetActive(true);
        ChangeState(StateEnum.Idle);
    }

    private void HandleDashEvent()
    {
        if (isAttack) return;

        ChangeState(StateEnum.Dash);
    }

    private void HandleAttackingEvent(bool isOnoff)
    {
        if (isOnoff)
            attackPoint.FadeInAttackPoint();
        else
            attackPoint.FadeOutAttackPoint();
    }

    private void Update()
    {
        stateDictionary[currentEnum].StateUpdate();

        GetWorldMousePosition();
    }

    public Vector3 GetWorldMousePosition()
    {
        mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);//스크린을 월드 좌표계로 바꾼다
        mouseDir.Normalize();
        return mouseDir;
    }

    public void FilpWeapon(bool value)
    {
        int flip = (value ? -1 : 1);

        if(flip == 1)
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        else
            GetComponentInChildren<SpriteRenderer>().flipX = true;

    }

    public void AttackWait()
    {
        StartCoroutine(AttackWaitRoutine());
    }

    public IEnumerator AttackWaitRoutine()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttack = true;
    }

    private void FixedUpdate()
    {
        stateDictionary[currentEnum].StateFixedUpdate();
    }

    private void HandleAttackEvent()
    {
        if (!isStopMove)
        {
            ChangeState(StateEnum.Attack);
            isStopMove = true;
        }
    }

    
    public void ChangeState(StateEnum newEnum)
    {
        print($"{currentEnum}에서 {newEnum}로");
        stateDictionary[currentEnum].Exit();
        currentEnum = newEnum;
        stateDictionary[currentEnum].Enter();
    }

    public void Damage(int damage)
    {
        if(!isUndead)
        {
            CurrentHp -= damage;
            if (CurrentHp == 0)
                Death();
        }
    }

    public void Death()
    {
        Time.timeScale = 0f;
        StartCoroutine(DeathWaitRoutine());
    }
    private IEnumerator DeathWaitRoutine()
    {
        yield return new WaitForSeconds(1f);
        SpawnManager.Instance.ReStart();
    }
}
