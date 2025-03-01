using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using System;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public static SpawnManager Instance;


    public CharacterSOList characterList;
    public GameObject currentPlayer;
    public CharacterSO currentChracter;
    [Header("�÷��̾� ������")]
    public GameObject player;
    [HideInInspector]
    public Transform spawnPoint;

    public Image skillUI;

    public Action OnSpawn;



    [Header("�ƾ�������")]
    [SerializeField]
    private Image spawningBee;
    [SerializeField]
    private GameObject cutScene;

    public bool IsGameStart { get; set; }

    [HideInInspector]
    public int idx;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Gamestart()
    {
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
        skillUI.gameObject.SetActive(true);
        skillUI.sprite = characterList.characters[idx].skillImage;
        currentChracter = characterList.characters[idx];
        yield return new WaitForSeconds(4f);

        cutScene.SetActive(false);
        OnSpawn?.Invoke();
    }
}
