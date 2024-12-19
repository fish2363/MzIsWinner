using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public CharacterSOList characterList;
    private GameObject currentPlayer;
    [Header("ÇÃ·¹ÀÌ¾î ÇÁ¸®ÆÕ")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private CinemachineVirtualCamera cinemachine;

    [Header("ÄÆ¾À¼³Á¤°ª")]
    [SerializeField]
    private Image spawningBee;
    [SerializeField]
    private GameObject cutScene;

    public bool IsGameStart { get; set; }

    int idx;

    private void Awake()
    {
        cinemachine = FindAnyObjectByType<CinemachineVirtualCamera>();
        ReStart();
    }

    public void ReStart()
    {
        if (currentPlayer != null)
            Destroy(currentPlayer);
        
        idx = Random.Range(0,characterList.characters.Count);
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
    }
}
