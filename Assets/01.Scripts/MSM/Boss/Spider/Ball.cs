using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float timer;
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
}
