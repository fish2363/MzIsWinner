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
        _player.PlayAnimaiton(AnimationType.PlayerAttackReady);
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        if(!_player.isStopMove)
            _player.RigidCompo.velocity = _player.inputReader.moveDir * _player.moveSpeed;


        if (_player.inputReader.moveDir == Vector2.zero)
        {
            print("Idle¹Ù²ñ");
            _player.ChangeState(StateEnum.Idle);
        }
    }


    public override void Exit()
    {
        base.Exit();
    }
}
