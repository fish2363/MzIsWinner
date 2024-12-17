using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    protected Player _player;
    public State(Player player)
    {
        _player = player;
    }

    public virtual void Enter()
    {

    }

    public virtual void StateUpdate()
    {

    }

    public virtual void StateFixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
