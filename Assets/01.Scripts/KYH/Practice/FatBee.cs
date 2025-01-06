using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBee : Bee
{
    public override void Skill(Player _player)
    {
        _player.healingParticle.Play(); //파티클 실행
        _player.SpriteCompo.DOColor(Color.green, 0.2f);
        SoundManager.Instance.ChangeMainStageVolume("Healing", true, ISOund.SFX);
        AnimationPlayer.Instance.PlayAnimaiton(_player.AnimatorCompo, "FatTired");

        _player.isUndead = true;
        _player.moveSpeed = 1f;
        _player.isSkillUsing = true;
        if (_player.CurrentHp != _player.MaxHp)
            _player.CurrentHp++;
    }
}
