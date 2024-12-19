using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBullet : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    [SerializeField] float speed;
    public void SetVelocity(float xGo)
    {
        rb.velocity = new Vector2 (Mathf.Sign(xGo), 0) * speed;
    }
}
