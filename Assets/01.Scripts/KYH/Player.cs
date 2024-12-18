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


public class Player : MonoBehaviour
{
    [field: SerializeField] public InputReader inputReader { get; private set; }

    [Header("�̵��ӵ�")]
    public float moveSpeed;

    [field: SerializeField] public Animator PlayerAnimator { get; private set; }
    public Rigidbody2D RigidCompo { get; private set; }
    public Animator AnimatorCompo { get; private set; }
    private Dictionary<StateEnum, State> stateDictionary = new Dictionary<StateEnum, State>();
    private StateEnum currentEnum;

    public float MaxHp { get { return maxHp; } }
    public float CurrentHp { get { return currentHp; } }
    [Header("�ִ� ü��")]
    [SerializeField]
    protected float maxHp;
    [Header("���� ü��")]
    [SerializeField]
    protected float currentHp;
    [Header("���� ������")]
    [SerializeField]
    private float attackDelay;

    [HideInInspector]
    public AttackPoint attackPoint;

    public Vector2 mouseDir { get; private set; }

    public bool isStopMove { get; set; }
    public bool isAttack { get; set; }


    private void Awake()
    {
        RigidCompo = GetComponent<Rigidbody2D>();
        AnimatorCompo = GetComponentInChildren<Animator>();
        attackPoint = GetComponentInChildren<AttackPoint>();

        foreach (StateEnum enumState in Enum.GetValues(typeof(StateEnum)))
        {
            Type t = Type.GetType($"{enumState}State");
            State state = Activator.CreateInstance(t, new object[] { this }) as State;
            stateDictionary.Add(enumState, state);
        }
        ChangeState(StateEnum.Idle);

        inputReader.OnAttackEvent += HandleAttackEvent;
        inputReader.OnAttackingEvent += HandleAttackingEvent;
        inputReader.OnDashEvent += HandleDashEvent;
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
        mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);//��ũ���� ���� ��ǥ��� �ٲ۴�
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
        stateDictionary[currentEnum].Exit();
        currentEnum = newEnum;
        stateDictionary[currentEnum].Enter();
    }
}
