using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Fall : MonoBehaviour
{
    [SerializeField] private Vector2 bounceDirection = Vector2.up;
    [SerializeField] private float bounceForce = 8f;
    [SerializeField] private float hitCooldown = 0.8f;
    [SerializeField] private float damageDelay = 0.2f;

    private bool isHitting;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (isHitting) return;
            isHitting = true;

            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.isStopMove = true;
                StartCoroutine(Enumerator(player));
            }
        }
    }

    IEnumerator Enumerator(Player player)
    {
        yield return new WaitForSeconds(damageDelay);

        player.Damage(1);
        ScreenShakeManager.Instance.ScreenShake(20, true, 0.2f, true, 0.2f);

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(bounceDirection.normalized * bounceForce, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(hitCooldown);
        isHitting = false;
        player.isStopMove = false;
    }
}
