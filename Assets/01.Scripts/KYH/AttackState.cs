using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(Player _agent) : base(_agent)
    {

    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        _player.RigidCompo.velocity = _player.mouseDir * _player.moveSpeed;
    }
}
