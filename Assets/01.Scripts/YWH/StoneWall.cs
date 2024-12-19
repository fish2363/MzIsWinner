using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StoneWall : MonoBehaviour
{

    bool already;
    [SerializeField] Transform stonewall, origin, move;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (already)
            {
                return;
            }

            already = true;

            ScreenShakeManager.Instance.ScreenShake(20, true, 0.2f, true, 0.8f);

            stonewall.DOMoveY(move.position.y, 2f).OnComplete(() =>
             {
                 stonewall.DOMoveY(origin.position.y, 0.5f).SetDelay(6);
             });
        }
    }
}
