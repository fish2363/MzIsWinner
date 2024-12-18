using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float timer;
    [SerializeField] LayerMask spit;
    [SerializeField] LayerMask mask;
    public Vector2 MovePos;
    private void Start()
    {
        MovePos = -transform.up.normalized;
        SetVelocity();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
    public void SetVelocity()
    {
        rb.velocity = MovePos * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & mask) != 0)
        {
            Ball b = collision.gameObject.GetComponent<Ball>();

            Vector3 income = b.MovePos;
            Vector3 normal = collision.contacts[0].normal;
            b.MovePos = Vector3.Reflect(income, normal).normalized;
            b.SetVelocity();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & spit) != 0)
        {
            rb.velocity *= new Vector2(1.05f, 1.05f);
        }
    }
}
