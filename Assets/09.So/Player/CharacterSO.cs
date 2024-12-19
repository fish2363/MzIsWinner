using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public enum CharacterType
{
    DashBee,
    StrongBee,
    FatBee,
    NinjaBee
}

[CreateAssetMenu(menuName = "CharacterSO/Chracter")]
public class CharacterSO : ScriptableObject
{
    public int maxHp;
    public float moveSpeed;
    public CharacterType character;
    public int beeIdx;
    public Sprite frontImage;
}
