using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(menuName = "CharacterSO/Chracter")]
public class CharacterSO : ScriptableObject
{
    public int maxHp;
    public float moveSpeed;
    public BeeType beeType;
    public int beeIdx;
    public Sprite frontImage;
    public Sprite skillImage;
    public int skillCool;
}
