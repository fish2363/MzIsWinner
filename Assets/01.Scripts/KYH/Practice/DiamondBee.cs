using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondBee : Bee
{
    public override void Skill(Player _player)
    {
        _player.isStopMove = true;
        _player.shieldParticle.Play();
        _player.isUndead = true;
        _player.Shield();
    }
}
