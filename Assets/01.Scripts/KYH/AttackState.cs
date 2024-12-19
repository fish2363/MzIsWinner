using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private Vector2 mouseDirect;
    private float attackSpeed = 2f;
    private float desiredAngle;

    public AttackState(Player _agent) : base(_agent)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _player.RigidCompo.velocity = Vector2.zero;
        AnimationPlayer.Instance.PlayAnimaiton(_player.AnimatorCompo,"PlayerAttackReady");
        //this.mouseDirect = _player.GetWorldMousePosition();

        _player.AttackWait();
        mouseDirect = _player.GetWorldMousePosition() - _player.transform.position;
        desiredAngle = Mathf.Atan2(mouseDirect.y, mouseDirect.x) * Mathf.Rad2Deg;

        _player.FilpWeapon(desiredAngle > 90f || desiredAngle < -90);
        AttackStart();
    }

    private void AttackStart()
    {
        ScreenShakeManager.Instance.AttackEffect();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        print(desiredAngle);
        if(_player.isAttack)
        {
            _player.RigidCompo.velocity = mouseDirect.normalized * attackSpeed;
            attackSpeed += Time.deltaTime * 5f;
        }

        Collider2D hit = Physics2D.OverlapCircle(_player.transform.position, _player.checkerRadius, _player.whatIsEntity);
        if (hit != null)
        {
            _player.Death();
            _player.RigidCompo.velocity = Vector2.zero;
            _player.isAttack = false;
        }
    }
}
