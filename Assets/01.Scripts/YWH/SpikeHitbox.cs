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
        if (!isFalling && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundManager.Instance.ChangeMainStageVolume("RockDrop", true, ISOund.SFX);
            if (isUp)
            spikeRigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            else
            spikeRigid.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
            isFalling = true;
        }
        
        
    }
}
