using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    public Transform player;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        transform.DOScale(new Vector2(0.18f, 0.7f), 0.3f);
        sprite.DOFade(1, 0.2f);

        RotateTowardsPlayer();
        StartCoroutine(Disappear());
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);

        rigid.velocity = direction.normalized * 10f;
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(4);
        transform.DOScale(Vector2.zero, 0.15f);
        sprite.DOFade(0, 0.2f).OnComplete(() => { Destroy(gameObject); });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScreenShakeManager.Instance.ScreenShake(20, true, 0.2f, true, 0.2f);
        transform.DOScale(new Vector2(0.18f * 3, 0.7f * 3), 0.13f);
        sprite.DOFade(0, 0.1f).OnComplete(() => { Destroy(gameObject); });
    }
}
