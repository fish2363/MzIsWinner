using UnityEngine;
using DG.Tweening;
using System.Collections;

public class GunFish : MonoBehaviour
{
    private Transform player;
    public float moveDistance = 5f;
    public float moveSpeed = 6f;
    public float randomOffset = 2f;
    public float popOutY = -1.96f;
    public float hideY = -3.65f;
    public float popOutDuration = 0.5f;
    public float fadeDuration = 0.3f;
    public float shootDelay = 1f;
    public float detectionRange = 10f;

    private SpriteRenderer spriteRenderer;
    public GameObject shootPrefab;

    private void Start()
    {
        SpawnManager.Instance.OnSpawn += FindPlayer;
    }
    public void FindPlayer()
    {
        print("Ff");
        player = FindAnyObjectByType<Player>().transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FishBehaviorLoop());
    }

    private IEnumerator FishBehaviorLoop()
    {
        while (true)
        {
            
            if (Vector3.Distance(transform.position, player.position) <= detectionRange)
            {
                yield return PopOut();
                yield return MoveAndShoot();
                yield return Hide();
            }
            else
            {
                yield return new WaitForSeconds(0.5f); 
            }
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
        spriteRenderer.DOFade(0f, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        float randomXOffset = Random.Range(-randomOffset, randomOffset);
        Vector3 targetPosition = new Vector3(player.position.x + randomXOffset, transform.position.y, transform.position.z);

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            RotateTowardsPlayer();

            yield return null;
        }

        spriteRenderer.DOFade(1f, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        RotateTowardsPlayer();
        yield return ShootAtPlayer();
    }

    private void RotateTowardsPlayer()
    {
        // 플레이어 방향 계산
        Vector3 direction = player.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        targetAngle = Mathf.Clamp(targetAngle, 50f, 140f);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0, 0, targetAngle),
            360f * Time.deltaTime
        );
    }



    private IEnumerator ShootAtPlayer()
    {
        transform.DOScale(transform.localScale * 0.8f, 0.3f);
        yield return new WaitForSeconds(1);
        transform.DOScale(transform.localScale * 1.25f, 0.3f);
        ScreenShakeManager.Instance.ScreenShake(5, true, 0.2f, true, 0.2f);
        SoundManager.Instance.ChangeMainStageVolume("WaterGun",true, ISOund.SFX);
        GameObject prefab = Instantiate(shootPrefab);
        prefab.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);

        Bullet bullet = prefab.GetComponent<Bullet>();
        bullet.player = player;

        Vector3 direction = player.position - prefab.transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        prefab.transform.rotation = Quaternion.Euler(0, 0, targetAngle);

        yield return new WaitForSeconds(shootDelay);
    }

    private void OnDisable()
    {
        SpawnManager.Instance.OnSpawn -= FindPlayer;
    }
}
