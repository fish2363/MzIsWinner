using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private Vector2 mouseDirect;
    private float attackSpeed = 2f;

    public AttackState(Player _agent) : base(_agent)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayAnimaiton(AnimationType.PlayerAttackReady);
        mouseDirect = _player.mouseDir;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        _player.RigidCompo.velocity = mouseDirect.normalized * attackSpeed;
        attackSpeed += Time.deltaTime;
    }
}
