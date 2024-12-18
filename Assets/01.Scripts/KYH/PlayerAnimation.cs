using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    Idle,
    Attack,
    Move,
}

public class PlayerAnimation : MonoBehaviour
{
    public Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    
}
