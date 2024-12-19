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

        if (_player.inputReader.direction == Vector3.zero)
            mouseDirect = Vector2.right;
        else
            mouseDirect = _player.inputReader.direction;


        if (_player.currentChracter.beeIdx == 1)
        {
            _player.isStopMove = true;
            _player.shieldParticle.Play();
            _player.isUndead = true;
            _player.Shield();
        }
        if (_player.currentChracter.beeIdx == 2)
        {
            _player.healingParticle.Play();
        }
    }
    

    public override void StateUpdate()
    {
        base.StateUpdate();
        if(_player.currentChracter.beeIdx == 0 || _player.currentChracter.beeIdx == 3)
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
            AnimationPlayer.Instance.PlayAnimaiton(_player.AnimatorCompo, "FatTired");
            _player.SpriteCompo.DOFade(0.3f, 0.2f);
            _player.moveSpeed = 1f;
            if(_player.CurrentHp != _player.MaxHp)
                _player.CurrentHp++;
            if (dashTime >= maxaDashTime)
            {
                dashTime = 0;
                _player.moveSpeed = _player.currentChracter.moveSpeed;
                _player.SpriteCompo.DOFade(1f, 0.2f);
                _player.isUndead = false;
                _player.ChangeState(StateEnum.Idle);
            }
        }
    }
}
