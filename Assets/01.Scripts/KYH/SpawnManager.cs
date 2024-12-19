using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Playables;
using System;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public CharacterSOList characterList;
    private GameObject currentPlayer;
    [Header("�÷��̾� ������")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform spawnPoint;

    public Action OnSpawn;

    [SerializeField]
    private CinemachineVirtualCamera cinemachine;

    private GameManager gameManager;

    [Header("�ƾ�������")]
    [SerializeField]
    private Image spawningBee;
    [SerializeField]
    private GameObject cutScene;

    public bool IsGameStart { get; set; }

    int idx;

    private void Start()
    {
        Gamestart();
    }

    public void Gamestart()
    {
        cinemachine = FindAnyObjectByType<CinemachineVirtualCamera>();
        
        ReStart();
    }

    public void ReStart()
    {
        if (currentPlayer != null)
            Destroy(currentPlayer);
        
        idx = UnityEngine.Random.Range(0,characterList.characters.Count);
        StartCoroutine(CutScene());
        print(idx);
        
    }

    private IEnumerator CutScene()
    {
        cutScene.SetActive(true);
        spawningBee.sprite = characterList.characters[idx].frontImage;
        yield return new WaitForSeconds(4f);

        cutScene.SetActive(false);

        currentPlayer = Instantiate(player, spawnPoint);
        currentPlayer.transform.SetParent(null);
        Player playerScript = currentPlayer.GetComponent<Player>();
        playerScript.currentChracter = characterList.characters[idx];
        cinemachine.Follow = playerScript.transform;
        OnSpawn?.Invoke();

        gameManager = GetComponent<GameManager>();
        gameManager.player = currentPlayer.transform;
    }
}
