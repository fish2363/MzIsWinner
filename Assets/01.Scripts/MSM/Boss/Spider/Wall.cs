using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]LayerMask mask;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & mask) != 0)
        {
            Ball b = collision.gameObject.GetComponent<Ball>();

            SoundManager.Instance.ChangeMainStageVolume("tongue", true, ISOund.SFX);

            Vector3 income = b.MovePos;
            Vector3 normal = collision.contacts[0].normal;
            b.MovePos = Vector3.Reflect(income, normal).normalized;
            b.SetVelocity();
        }
    }
}
