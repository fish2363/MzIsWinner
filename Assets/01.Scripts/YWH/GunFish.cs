using UnityEngine;
using DG.Tweening;
using System.Collections;

public class GunFish : MonoBehaviour
{
    public Transform player;
    public float moveDistance = 5f;
    public float moveSpeed = 3f;
    public float randomOffset = 2f;
    public float popOutY = -1.96f;
    public float hideY = -3.65f;
    public float popOutDuration = 0.5f;
    public float fadeDuration = 0.5f;
    public float shootDelay = 1f;

    private SpriteRenderer spriteRenderer;
    public GameObject shootPrefab;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FishBehaviorLoop());
    }

    private IEnumerator FishBehaviorLoop()
    {
        while (true)
        {
            yield return PopOut();
            yield return MoveAndShoot();
            yield return Hide();
        }
    }

    private IEnumerator PopOut()
    {
        spriteRenderer.DOFade(1f, fadeDuration);
        transform.DOMoveY(popOutY, popOutDuration).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(popOutDuration);
    }

    private IEnumerator Hide()
    {
        spriteRenderer.DOFade(0f, fadeDuration);
        transform.DOMoveY(hideY, popOutDuration).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(popOutDuration);
    }

    private IEnumerator MoveAndShoot()
    {
        float randomXOffset = Random.Range(-randomOffset, randomOffset);
        Vector3 targetPosition = new Vector3(player.position.x + randomXOffset, transform.position.y, transform.position.z);

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            RotateTowardsPlayer();
            yield return null;
        }

        yield return ShootAtPlayer();
        yield return Hide();
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, targetAngle), 360f * Time.deltaTime);
    }

    private IEnumerator ShootAtPlayer()
    {
        transform.DOScale(transform.localScale * 0.8f, 0.3f);
        yield return new WaitForSeconds(1);
        transform.DOScale(transform.localScale * 1.25f, 0.3f);
        ScreenShakeManager.Instance.ScreenShake(5, true, 0.2f, true, 0.2f);

        GameObject prefab = Instantiate(shootPrefab);
        prefab.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);

        Bullet bullet = prefab.GetComponent<Bullet>();
        bullet.player = player;

        Vector3 direction = player.position - prefab.transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        prefab.transform.rotation = Quaternion.Euler(0, 0, targetAngle);

        yield return new WaitForSeconds(shootDelay);
    }

}
