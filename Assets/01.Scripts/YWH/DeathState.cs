using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public DeathState(Player _agent) : base(_agent)
    {

    }

    public override void Enter()
    {
        _player.isStopMove = true;
        _player.isAttackLock = true;
        AnimationPlayer.Instance.PlayAnimaiton(_player.AnimatorCompo, "PlayerDeath");
        Time.timeScale = 0.2f;
        _player.DeathWait();
    }
}