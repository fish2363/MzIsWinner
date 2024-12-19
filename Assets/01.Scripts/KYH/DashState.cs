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

        if(_player.currentChracter.beeIdx == 1)
        {
            _player.isStopMove = true;
            _player.bohoRenderer.DOFade(0.5f, 0.2f);
            _player.isUndead = true;
            _player.Shield();
        }
    }

    

    public override void StateUpdate()
    {
        base.StateUpdate();
        if(_player.currentChracter.beeIdx == 0)
        {
            dashTime += Time.deltaTime;
            _player.isUndead = true;
            _player.SpriteCompo.DOFade(0.3f, 0.2f);
            _player.RigidCompo.velocity = mouseDirect.normalized * _player.DashPower;

            if (dashTime >= maxaDashTime)
            {
                dashTime = 0;
                _player.SpriteCompo.DOFade(1f, 0.2f);
                _player.isUndead = false;
                _player.ChangeState(StateEnum.Idle);
            }
        }
        if (_player.currentChracter.beeIdx == 2)
        {
            dashTime += Time.deltaTime;
            _player.isUndead = true;
            _player.SpriteCompo.DOFade(0.3f, 0.2f);
            _player.RigidCompo.velocity = Vector2.down * _player.DashPower;

            if (dashTime >= maxaDashTime)
            {
                dashTime = 0;
                _player.SpriteCompo.DOFade(1f, 0.2f);
                _player.isUndead = false;
                _player.ChangeState(StateEnum.Idle);
            }
        }
    }
}
