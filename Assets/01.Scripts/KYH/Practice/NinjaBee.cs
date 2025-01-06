using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaBee : Bee
{
    public override void Skill(Player _player)
    {
        _player.isUndead = true;
        _player.SpriteCompo.DOFade(0.3f, 0.2f);
        _player.isSkillUsing = true;
    }
}
