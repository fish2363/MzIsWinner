using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillState : State
{
    public SkillState(Player _agent) : base(_agent)
    {

    }

    public override void Enter()
    {
        _player.RigidCompo.velocity = Vector2.zero;

        if (_player.inputReader.direction == Vector3.zero) _player.mouseDirect = Vector2.right;
        else _player.mouseDirect = _player.inputReader.direction;

        SkillSetter skill = new SkillSetter();
        skill.UseSkill(_player.currentBee, _player);
        //SpawnManager.Instance.skillUI.DOColor(Color.grey, 0.2f);
    }

    public override void Exit()
    {
        _player.RigidCompo.velocity = Vector2.zero;
    }
}
