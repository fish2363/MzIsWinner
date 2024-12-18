using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHitbox : MonoBehaviour
{
    private Rigidbody2D spikeRigid;
    private bool isFalling;
    [SerializeField] bool isUp;

    private void Awake()
    {
        spikeRigid = GetComponentInChildren<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFalling)
        {
            if (isUp)
            spikeRigid.AddForce(Vector2.up * Random.Range(5, 13), ForceMode2D.Impulse);
            else
            spikeRigid.AddForce(Vector2.down * Random.Range(5, 13), ForceMode2D.Impulse);
            isFalling = true;
        }
        
        
    }
}
