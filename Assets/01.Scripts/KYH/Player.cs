using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateEnum
{
    Idle,
    Move,
    Attack
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

    [Header("이동속도")]
    public float moveSpeed;

    [field: SerializeField] public Animator PlayerAnimator { get; private set; }
    public Rigidbody2D RigidCompo { get; private set; }
    private Dictionary<StateEnum, State> stateDictionary = new Dictionary<StateEnum, State>();
    private StateEnum currentEnum;

    public float MaxHp { get { return maxHp; } }
    public float CurrentHp { get { return currentHp; } }
    [Header("최대 체력")]
    [SerializeField]
    protected float maxHp;
    [Header("현재 체력")]
    [SerializeField]
    protected float currentHp;
    [Header("공격 딜레이")]
    [SerializeField]
    private float attackDelay;

    private AttackPoint attackPoint;

    public Vector2 mouseDir { get; private set; }

    public bool isStopMove { get; set; }
    public bool isAttack { get; set; }


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
        ChangeState(StateEnum.Idle);

        inputReader.OnAttackEvent += HandleAttackEvent;
        inputReader.OnAttackingEvent += HandleAttackingEvent;
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

    public void PlayAnimaiton(AnimationType animationType)
    {
        Play(animationType);
    }

    internal void StopAnimation()
    {
        PlayerAnimator.enabled = false;
    }
    internal void StartAnimation()
    {
        PlayerAnimator.enabled = true;
    }

    public void Play(AnimationType name)
    {
        PlayerAnimator.Play(name.ToString());
    }

    public void ChangeState(StateEnum newEnum)
    {
        stateDictionary[currentEnum].Exit();
        currentEnum = newEnum;
        stateDictionary[currentEnum].Enter();
    }
}
