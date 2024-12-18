using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Bat : MonoBehaviour
{
    [SerializeField]Transform targetTrans;
    [SerializeField]Vector2 distance;
    [SerializeField] float _timerMin , _timerMax;
    [SerializeField] private float speed;
    [SerializeField] private float checkerRadius;
    [SerializeField] LayerMask player;
    private Rigidbody2D rb;
    private bool once = true;
    float _timer;
    float _time;
    private bool end = false;
    [SerializeField]private bool isFind = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _timer = Random.Range(_timerMin, _timerMax);
    }
    private void Update()
    {
        if (isFind)
            _timer -= Time.deltaTime;
        Collider2D hit = Physics2D.OverlapCircle(transform.position, checkerRadius, player);
        if(hit != null)
        {
            isFind = true;
            targetTrans = hit.transform;
        }
    }
    private void FixedUpdate()
    {
        if (isFind)
        {
            Vector3 moveDir;
            if(_timer <= 0&& once)
            {
                moveDir = targetTrans.position - transform.position;
                rb.velocity = moveDir.normalized * speed * 3;
                once = false;
            }
            else if (once)
            {
                _time += Time.deltaTime;
                float X = Mathf.Sin(_time * speed) * distance.x;
                moveDir = new Vector3(X/2f, distance.y, 0f);
                transform.DOMove(targetTrans.position + moveDir,1f);
                //transform.position = targetTrans.position + moveDir;
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkerRadius);
        Gizmos.color = Color.white;
    }
}
