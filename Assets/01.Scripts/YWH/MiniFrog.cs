using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MiniFrog : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

       
        StartCoroutine(JumpAndMove());
    }

    private IEnumerator JumpAndMove()
    {
        while (true)
        {
            Vector3 playerPosition = GameManager.Instance.GetPlayerPosition();  // 플레이어의 위치 얻기
            Vector3 direction = (playerPosition - transform.position).normalized;  // 플레이어 방향 계산

            float jumpForce = Random.Range(5f, 7f);
            float moveSpeed = direction.x > 0 ? Random.Range(3f, 5f) : Random.Range(-5f, -3f);  // 방향에 맞는 속도 설정
            float jumpDuration = Random.Range(0.5f, 2f);

            Flip(moveSpeed);

            animator.Play("FrogJump");

            rb.velocity = new Vector2(moveSpeed, jumpForce);
            float jumpTimer = 0f;

            while (jumpTimer < jumpDuration)
            {
                jumpTimer += Time.deltaTime;
                rb.velocity = new Vector2(moveSpeed, Mathf.Lerp(jumpForce, 0f, jumpTimer / jumpDuration));
                yield return null;
            }
           
            rb.velocity = new Vector2(moveSpeed, 0f);

            // 점프 후 잠시 대기
            yield return new WaitForSeconds(0.5f);
            animator.Play("FrogIdle");
            yield return new WaitForSeconds(2f);
        }
    }

    private void Flip(float moveSpeed)
    {
        // moveSpeed에 따라 물고기의 방향을 바꿔줍니다
        if (moveSpeed < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (moveSpeed > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            animator.Play("FrogExplo");
            transform.DOScale(transform.localScale * 1.1f, 0.05f);
            transform.DOShakeRotation(0.3f, 1, 10, 90);

            ScreenShakeManager.Instance.ScreenShake(30, true, 0.3f, true, 0.2f);

            Destroy(gameObject, 0.5f);
        }
    }
}
