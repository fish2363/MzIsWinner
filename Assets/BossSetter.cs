using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossSetter : MonoBehaviour
{
    public Transform spawnPoint;
    private CinemachineVirtualCamera cinemachine;

    private void Awake()
    {
        cinemachine = FindAnyObjectByType<CinemachineVirtualCamera>();
    }


    private void Start()
    {
        if(SpawnManager.Instance.currentChracter == null)
        {
            SpawnManager.Instance.Gamestart();
        }
        GameObject player = Instantiate(SpawnManager.Instance.player, spawnPoint);
        //currentPlayer.transform.SetParent(null);
        Player playerScript = player.GetComponent<Player>();
        playerScript.currentChracter = SpawnManager.Instance.currentChracter;
        cinemachine.Follow = playerScript.transform;
        playerScript.isStopMove = true;
    }
}
