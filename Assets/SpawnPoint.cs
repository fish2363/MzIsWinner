using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        SpawnManager.Instance.spawnPoint = transform;
    }
}
