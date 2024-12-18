using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] Transform targetTrans;
    [SerializeField]Vector2 distance;
    [SerializeField] float _timerMin , _timerMax;
    [SerializeField] private float speed;
    [SerializeField] private float checkerRadius;
    [SerializeField] LayerMask player;
    private Rigidbody2D rb;
    private bool once = true;
    float _timer;
    private bool end = false;
    private bool isFind = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _timer = Random.Range(_timerMin, _timerMax);
    }
    private void OnEnable()
    {
        if(targetTrans.position.x - transform.position.x > -distance.x)
        {
            distance.x = Mathf.Abs(distance.x);
        }
        else
        {
            distance.x = -Mathf.Abs(distance.x);
        }
    }
    private void Update()
    {
        _timer -= Time.deltaTime;
        if(Physics2D.CircleCastAll(transform.position,checkerRadius,Vector2.zero,player) != null)
        {
            isFind = true;
        }
    }
    private void FixedUpdate()
    {
        if (isFind)
        {
            Vector2 moveDir;
            if(_timer <= 0&& once)
            {
                moveDir = targetTrans.position - transform.position;
                rb.velocity = moveDir.normalized * speed * 3;
                once = false;
            }
            else if (once)
            {
                if (targetTrans.position.x - transform.position.x > -distance.x)
                {
                    distance.x = Mathf.Abs(distance.x);
                }
                else
                {
                    distance.x = -Mathf.Abs(distance.x);
                }
                moveDir = new Vector2(targetTrans.position.x - transform.position.x + distance.x , targetTrans.position.y - transform.position.y + distance.y);
                transform.position += (Vector3)(moveDir.normalized * speed * Time.deltaTime);
            }
            else if(end)
            {
                moveDir = new Vector2(-1,1);
                rb.velocity = moveDir.normalized * speed* 10;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        end = true;
    }
}
