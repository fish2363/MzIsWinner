using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossSetter : MonoBehaviour
{
    public Transform spawnPoint;
    private CinemachineVirtualCamera cinemachine;
    Player playerScript;

    private void Awake()
    {
        cinemachine = FindAnyObjectByType<CinemachineVirtualCamera>();
    }


    private void Start()
    {
        GameObject player = Instantiate(SpawnManager.Instance.player, spawnPoint);
        //currentPlayer.transform.SetParent(null);
        playerScript = player.GetComponent<Player>();
        playerScript.currentChracter = SpawnManager.Instance.currentChracter;
        cinemachine.Follow = playerScript.transform;
        playerScript.isStopMove = true;
    }
    public void CameraShake()
    {
        ScreenShakeManager.Instance.ScreenShake();
    }
    public void StartBoss()
    {
        playerScript.isStopMove = false;
    }
}
