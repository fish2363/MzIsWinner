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
        AnimationPlayer.Instance.PlayAnimaiton(_player.AnimatorCompo,"PlayerAttackReady");
        mouseDirect = _player.GetWorldMousePosition();

        _player.AttackWait();
        Vector3 airDir = _player.GetWorldMousePosition() - _player.transform.position;
        desiredAngle = Mathf.Atan2(airDir.y, airDir.x) * Mathf.Rad2Deg;

        _player.FilpWeapon(desiredAngle > 90f || desiredAngle < -90);
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
    }
}
