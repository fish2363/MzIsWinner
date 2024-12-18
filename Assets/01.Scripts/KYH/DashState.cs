using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DashState : State
{
    private float maxaDashTime = 0.2f;
    private float dashTime;
    private Vector2 mouseDirect;

    public DashState(Player _agent) : base(_agent)
    {

    }

    public override void Enter()
    {
        base.Enter();
        print("´ë½¬");
        _player.RigidCompo.velocity = Vector2.zero;
        mouseDirect = _player.inputReader.direction;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        dashTime += Time.deltaTime;
        _player.isUndead = true;
        _player.AnimatorCompo.gameObject.GetComponent<SpriteRenderer>().DOFade(0.3f, 0.5f);
        _player.RigidCompo.velocity = mouseDirect.normalized * _player.DashPower;

        if (dashTime >= maxaDashTime)
        {
            dashTime = 0;
            _player.AnimatorCompo.gameObject.GetComponent<SpriteRenderer>().DOFade(1f, 1);
            _player.isUndead = false;
            _player.ChangeState(StateEnum.Idle);
        }
    }
}
