using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class FishMovement : MonoBehaviour
{
    public Transform startPoint;
    public Transform targetPoint;
    public float heightMin = 2f;
    public float heightMax = 5f;
    public float playerDetectionRadius = 5f; // 플레이어 감지 범위

    private bool isMovingToTarget = true;
    private SpriteRenderer sprite;
    private Vector3 originalScale;
    private int attackDamage = 1;
    private SpriteRenderer mark;
    private Transform player;

    private void Awake()
    {
        SpawnManager.Instance.OnSpawn += FindPlayer;
    }

    public void FindPlayer()
    {
        sprite = GetComponent<SpriteRenderer>();
        mark = startPoint.GetChild(0).GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        StartCoroutine(MoveFish());
    }

    private IEnumerator MoveFish()
    {
        while (true)
        {
            
            while (!CheckForPlayer())
            {
                yield return new WaitForSeconds(0.5f); 
            }

            yield return new WaitForSeconds(Random.Range(0, 1));
            Transform from = isMovingToTarget ? startPoint : targetPoint;
            Transform to = isMovingToTarget ? targetPoint : startPoint;

            float height = Random.Range(heightMin, heightMax);
            yield return StartCoroutine(ParabolicMove(from.position, to.position, height, 3f));
            isMovingToTarget = !isMovingToTarget;
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    private bool CheckForPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, playerDetectionRadius, LayerMask.GetMask("Player"));
        if (playerCollider != null)
        {
            player = playerCollider.transform; 
            return true;
        }
        return false;
    }

    private IEnumerator ParabolicMove(Vector3 start, Vector3 end, float height, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            Light2D light = mark.GetComponent<Light2D>();
            DOTween.To(() => light.intensity, x => light.intensity = x, 1, 0.12f);
            mark.DOFade(1, 0.12f).OnComplete(() =>
            {
                mark.DOFade(0, 0.12f);
                DOTween.To(() => light.intensity, x => light.intensity = x, 0, 0.12f);
            });
            float t = time / duration;
            Vector3 linearPosition = Vector3.Lerp(start, end, t);
            float parabolicHeight = Mathf.Sin(Mathf.PI * t) * height;
            transform.position = new Vector3(linearPosition.x, linearPosition.y + parabolicHeight, linearPosition.z);
            time += Time.deltaTime;
            yield return null;

            
        }
        transform.position = end;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            sprite.DOColor(Color.red, 0.1f);
            sprite.DOColor(Color.white, 0.1f).SetDelay(0.3f);
            transform.DOScale(originalScale * 1.7f, 0.1f).OnComplete(() => transform.DOScale(originalScale, 0.1f));
            transform.DOShakeRotation(0.3f, 1, 10, 90);
            ScreenShakeManager.Instance.ScreenShake(30, true, 0.3f, true, 0.2f);
            IDamage damage = collision.GetComponent<IDamage>();

            if (damage != null)
            {
                damage.Damage(attackDamage);
            }
        }
    }

    private void OnDisable()
    {
        SpawnManager.Instance.OnSpawn -= FindPlayer;
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
