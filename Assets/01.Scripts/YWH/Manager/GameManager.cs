using System.Collections;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform player;

    public Vector3 GetPlayerPosition()
    {
        return player.position; 
    }
}
