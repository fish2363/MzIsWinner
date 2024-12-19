using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public CharacterSOList characterList;
    private GameObject currentPlayer;
    [Header("플레이어 프리팹")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private CinemachineVirtualCamera cinemachine;

    private void Awake()
    {
        ReStart();
    }

    public void ReStart()
    {
        if (currentPlayer != null)
            Destroy(currentPlayer);
        currentPlayer = Instantiate(player,spawnPoint);
        currentPlayer.transform.SetParent(null);
        int idx = Random.Range(0,characterList.characters.Count);
        CutScene();
        Player playerScript = currentPlayer.GetComponent<Player>();
        playerScript.currentChracter = characterList.characters[idx];
        cinemachine.Follow = playerScript.transform;
    }

    private void CutScene()
    {

    }
}
