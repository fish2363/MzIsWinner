using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBee : Bee
{
    public override void Skill(Player _player)
    {
        _player.SpriteCompo.DOFade(0.3f, 0.2f);
        _player.isUndead = true;
        _player.isSkillUsing = true;
    }
}
