using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHitbox : MonoBehaviour
{
    private Rigidbody2D spikeRigid;
    private bool isFalling;

    private void Awake()
    {
        spikeRigid = GetComponentInChildren<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFalling)
        {
            spikeRigid.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
            isFalling = true;
        }
        
        
    }
}
