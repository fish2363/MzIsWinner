using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    public Transform player;
    private bool isHitting;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        transform.DOScale(new Vector2(1, 1), 0.3f);
        sprite.DOFade(1, 0.2f);

        RotateTowardsPlayer();
        StartCoroutine(Disappear());
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);

        rigid.velocity = direction.normalized * 20f;
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(7);
        transform.DOScale(Vector2.zero, 0.15f);
        sprite.DOFade(0, 0.2f).OnComplete(() => { Destroy(gameObject); });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            if (isHitting)
            {
                return;
            }
            isHitting = true;
            collision.GetComponent<Player>().Damage(1);

            ScreenShakeManager.Instance.ScreenShake(20, true, 0.2f, true, 0.2f);
            transform.DOScale(new Vector2(1 * 1.5f, 1 * 1.5f), 0.13f);
            sprite.DOFade(0, 0.1f).OnComplete(() => { Destroy(gameObject); });
            
           
        
        }
        
    }
}
