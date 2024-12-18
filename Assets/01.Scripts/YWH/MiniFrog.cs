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

        animator.Play("FrogJump");
        StartCoroutine(JumpAndMove());
    }

    private IEnumerator JumpAndMove()
    {
        float jumpForce = Random.Range(3,10);
        float moveSpeed = Random.Range(-3, -5);
        float jumpDuration = Random.Range(0.5f, 2);

        rb.velocity = new Vector2(moveSpeed, jumpForce);
        float jumpTimer = 0f;

        while (jumpTimer < jumpDuration)
        {
            jumpTimer += Time.deltaTime;
            rb.velocity = new Vector2(moveSpeed, Mathf.Lerp(jumpForce, 0f, jumpTimer / jumpDuration));
            yield return null;
        }

        rb.velocity = new Vector2(moveSpeed, 0f);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        { 
            animator.Play("FrogExplo");
            transform.DOScale(transform.localScale * 1.1f,0.05f);
            transform.DOShakePosition(0.3f, 1, 10, 90);

            ScreenShakeManager.Instance.ScreenShake(30, true, 0.3f, true, 0.2f);
            
        }

    }

}
