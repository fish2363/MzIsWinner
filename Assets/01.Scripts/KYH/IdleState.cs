using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Player _agent) : base(_agent)
    {

    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        print(_player.inputReader.moveDir.x);
        if (_player.inputReader.moveDir.x > 0 || _player.inputReader.moveDir.x < 0)
            _player.ChangeState(StateEnum.Move);
    }
}
