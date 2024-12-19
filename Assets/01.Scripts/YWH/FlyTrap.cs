using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTrap : MonoBehaviour
{
    Animator flyTrap;
    PointEffector2D pointEffector;
    bool isAttaking;

    private void Awake()
    {
        flyTrap = GetComponentInChildren<Animator>();
        pointEffector = flyTrap.GetComponentInChildren<PointEffector2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (isAttaking)
            {
                return;
            }

            isAttaking = true;
            Player player = collision.GetComponent<Player>();
            player.isStopMove = true;
            pointEffector.enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            StartCoroutine(Coroutine(player));
        }
    }

    IEnumerator Coroutine(Player player)
    {

        flyTrap.Play("Atk");
        yield return new WaitForSeconds(0.2f);

        player.Damage(1);
        ScreenShakeManager.Instance.ScreenShake(20, true, 0.2f, true, 0.2f);
 
        player.isStopMove = false;
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 100, ForceMode2D.Force);

        yield return new WaitForSeconds(1.5f);
        isAttaking = false;
        pointEffector.enabled = true;
        flyTrap.Play("Idle");
    }
}
