using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Player _agent) : base(_agent)
    {

    }

    public override void Exit()
    {
        base.Exit();
        _player.RigidCompo.velocity = Vector3.zero;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        print(_player.inputReader.moveDir.x);
        if (_player.inputReader.moveDir != Vector2.zero)
        {
            _player.ChangeState(StateEnum.Move);
            print("move¹Ù²ñ");
        }
    }
}
