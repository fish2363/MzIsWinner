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
    [SerializeField]
    private float moveSpeed;

    private Dictionary<StateEnum, State> stateDictionary = new Dictionary<StateEnum, State>();
    private StateEnum curretEnum;

    public InputReader InputReader { get; private set; }
    public Rigidbody2D RigidCompo { get; private set; }

    private void Awake()
    {
        foreach(StateEnum stateEnum in Enum.GetValues(typeof(StateEnum)))
        {

        }
    }

    public void ChangeState(StateEnum change)
    {
        stateDictionary[curretEnum].Exit();
        curretEnum = change;
        stateDictionary[curretEnum].Enter();
    }
}
