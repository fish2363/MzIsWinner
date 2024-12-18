using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }
}
