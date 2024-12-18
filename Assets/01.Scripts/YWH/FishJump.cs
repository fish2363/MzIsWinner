using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FishMovement : MonoBehaviour
{
    public Transform startPoint;
    public Transform targetPoint;
    public float heightMin = 2f;
    public float heightMax = 5f;

    private bool isMovingToTarget = true;
    private SpriteRenderer sprite;
    private Vector3 originalScale;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        StartCoroutine(MoveFish());
    }

    private IEnumerator MoveFish()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0, 3));
            Transform from = isMovingToTarget ? startPoint : targetPoint;
            Transform to = isMovingToTarget ? targetPoint : startPoint;

            float height = Random.Range(heightMin, heightMax);
            yield return StartCoroutine(ParabolicMove(from.position, to.position, height, 1.5f));
            isMovingToTarget = !isMovingToTarget;
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    private IEnumerator ParabolicMove(Vector3 start, Vector3 end, float height, float duration)
    {
        float time = 0;
        while (time < duration)
        {
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
        }
    }
}
