using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    private float maxaDashTime = 0.3f;
    private float dashTime;
    private bool isDash;

    public DashState(Player _agent) : base(_agent)
    {

    }

    public override void Enter()
    {
        base.Enter();
        print("´ë½¬");
        _player.RigidCompo.velocity = Vector2.zero;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        dashTime += Time.deltaTime;
        isDash = true;

        _player.RigidCompo.velocity = _player.inputReader.direction * _player.DashPower;

        if (dashTime >= maxaDashTime)
        {
            dashTime = 0;
            isDash = false;
            _player.ChangeState(StateEnum.Idle);
        }
    }
}
