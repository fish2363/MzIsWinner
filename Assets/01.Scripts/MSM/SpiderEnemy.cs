using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiderEnemy : MonoBehaviour
{
    [SerializeField] Transform MaxTrans;
    [SerializeField] Transform MinTrans;
    [SerializeField] LayerMask Player;
    [SerializeField] float radus;
    [SerializeField] float runDistance;
    [SerializeField] float AttackTime;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] float speed;
    private Rigidbody2D rb;
    float _timer;
    Transform PlayerTrans;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position , radus,Player);
        if(collider != null)
        {
            PlayerTrans = collider.transform;
            _timer -= Time.deltaTime;
            if(_timer <= 0 && MaxTrans.position.y + 0.6f > PlayerTrans.position.y && MinTrans.position.y - 0.6f >= PlayerTrans.position.y)
            {
                _timer = AttackTime;
                Transform bullet = Instantiate(BulletPrefab , transform.position , Quaternion.identity).transform;
                bullet.GetComponent<SpiderBullet>().SetVelocity(PlayerTrans.position.x > transform.position.x? 1 : -1);
            }
        }
        else
        {
            PlayerTrans = null;
            _timer = AttackTime;
        }
    }
    private void FixedUpdate()
    {
        if (PlayerTrans != null)
        {
            if (Vector2.Distance(transform.position, PlayerTrans.position) <= runDistance)
            {
                if(MaxTrans.position.y <= transform.position.y)
                {
                    rb.velocity = Vector2.zero;
                    transform.position = new Vector2(transform.position.x , MaxTrans.position.y);
                }
                else
                {
                    rb.velocity = new Vector2(0, MaxTrans.position.y - transform.position.y) * speed;
                }
            }
            else
            {
                if (MaxTrans.position.y <= transform.position.y && MaxTrans.position.y <= PlayerTrans.position.y)
                {
                    rb.velocity = Vector2.zero;
                    transform.position = new Vector2(transform.position.x, MaxTrans.position.y);
                }
                else if(MinTrans.position.y >= transform.position.y && MinTrans.position.y >= PlayerTrans.position.y)
                {
                    rb.velocity = Vector2.zero;
                    transform.position = new Vector2(transform.position.x, MinTrans.position.y);
                }
                else
                {
                    rb.velocity = new Vector2(0, PlayerTrans.position.y - transform.position.y) * speed;
                }
            }
        }
        else
        {
            if (MaxTrans.position.y <= transform.position.y)
            {
                rb.velocity = Vector2.zero;
                transform.position = new Vector2(transform.position.x, MaxTrans.position.y);
            }
            else
            {
                rb.velocity = new Vector2(0, MaxTrans.position.y - transform.position.y) * speed;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radus);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, runDistance);
        Gizmos.color = Color.white;
    }
}
