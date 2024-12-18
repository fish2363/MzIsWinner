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

public class Player : MonoBehaviour
{
    public InputReader inputReader;
    public float moveSpeed;

    public Rigidbody2D RigidCompo { get; private set; }
    private Dictionary<StateEnum, State> stateDictionary = new Dictionary<StateEnum, State>();
    private StateEnum currentEnum;

    public float MaxHp { get { return maxHp; } }
    public float CurrentHp { get { return currentHp; } }
    [SerializeField]
    protected float maxHp;
    [SerializeField]
    protected float currentHp;

    public bool isAttack { get; set; }

    private void Awake()
    {
        RigidCompo = GetComponent<Rigidbody2D>();

        foreach (StateEnum enumState in Enum.GetValues(typeof(StateEnum)))
        {
            Type t = Type.GetType($"{enumState}State");
            State state = Activator.CreateInstance(t, new object[] { this }) as State;
            stateDictionary.Add(enumState, state);
        }
        ChangeState(StateEnum.Idle);

        inputReader.OnAttackEvent += HandleAttackEvent;
    }

    private void Update()
    {
        stateDictionary[currentEnum].StateUpdate();
    }

    private void FixedUpdate()
    {
        stateDictionary[currentEnum].StateFixedUpdate();
    }

    private void HandleAttackEvent()
    {
        if (!isAttack)
        {
            ChangeState(StateEnum.Attack);
            isAttack = true;
        }
    }

    public void ChangeState(StateEnum newEnum)
    {
        print($"{currentEnum}¹Ù²ñ");
        stateDictionary[currentEnum].Exit();
        currentEnum = newEnum;
        stateDictionary[currentEnum].Enter();
    }
}
