using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public MoveState(Player _agent) : base(_agent)
    {

    }

    public override void Enter()
    {
        base.Enter();
        AnimationPlayer.Instance.PlayAnimaiton(_player.AnimatorCompo, "PlayerMove");
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        if(!_player.isStopMove)
            _player.RigidCompo.velocity = _player.inputReader.moveDir * _player.moveSpeed;

        if (_player.inputReader.moveDir.x > 0)
            _player.GetComponentInChildren<SpriteRenderer>().flipX = false;
        else if (_player.inputReader.moveDir.x < 0)
            _player.GetComponentInChildren<SpriteRenderer>().flipX = true;


        if (_player.inputReader.moveDir == Vector2.zero)
        {
            _player.ChangeState(StateEnum.Idle);
        }
    }


    public override void Exit()
    {
        base.Exit();
    }
}
