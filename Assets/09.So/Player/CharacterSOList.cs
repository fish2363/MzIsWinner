using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterSO/ChracterSOList")]
public class CharacterSOList : ScriptableObject
{
    public List<CharacterSO> characters = new List<CharacterSO>();
}
