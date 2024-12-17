using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Player player) : base(player)
    {
        _player = player;
    }

    public override void Enter()
    {
        base.Enter();
        _player.RigidCompo.velocity = Vector3.zero;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
