using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public CharacterSOList characterList;
    private Player player;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        ReStart();
    }

    public void ReStart()
    {
        int idx = Random.Range(0,characterList.characters.Count);
        player.currentChracter = characterList.characters[idx];

    }
}
