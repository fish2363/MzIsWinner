using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float _timer;
    private void Start()
    {
        rb.velocity = transform.up * speed;
    }
    private void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
