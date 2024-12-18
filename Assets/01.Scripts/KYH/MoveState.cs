using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public MoveState(Player _agent) : base(_agent)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _player.inputReader.OnMoveEvent += OnMove;
    }

    private void OnMove(Vector2 obj)
    {
        _player.RigidCompo.velocity = obj * _player.moveSpeed;
    }

    public override void Exit()
    {
        _player.inputReader.OnMoveEvent -= OnMove;
        base.Exit();
    }
}
