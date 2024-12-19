using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class BearStamp : MonoBehaviour
{
    [SerializeField] Transform Y;
    [SerializeField] Transform UpY;
    [SerializeField] Transform DownY;
    private float upY;
    private float downY;
    private float y;
    [SerializeField] float timeRandMin;
    [SerializeField] float timeRandMax;
    [SerializeField] float BearSpeed;
    Rigidbody2D rb;
    private Transform target;

    [SerializeField]
    private int attackDamage;

    private void Awake()
    {
        y = Y.position.y;
        upY = UpY.position.y;
        downY = DownY.position.y;
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(0, upY + 3, 0);
    }
    public void SetTarget(Transform targetTrans)
    {
        target = targetTrans;
    }
    public IEnumerator Attack()
    {
        transform.position = new Vector3(0, upY + 3, 0);
        float timer = Random.Range(timeRandMin, timeRandMax);
        while (Mathf.Abs(transform.position.y - y) > 0.1f)
        {
            rb.velocity = new Vector3(0, y - transform.position.y, 0).normalized * BearSpeed * 2f;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x,y);


        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if(Mathf.Abs(transform.position.x - target.position.x) > 0.1f)
            {
                rb.velocity = new Vector3(target.position.x - transform.position.x, 0, 0).normalized * BearSpeed;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
            yield return null;
        }


        while (Mathf.Abs(transform.position.y - upY) > 0.1f)
        {
            rb.velocity = new Vector3(0, upY - transform.position.y, 0).normalized * BearSpeed *2f;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, upY);


        while (transform.position.y > downY)
        {
            rb.velocity = new Vector3(0, downY - transform.position.y, 0).normalized * BearSpeed * 5;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, downY);


        while (transform.position.y < upY + 3)
        {
            rb.velocity = new Vector3(0, upY - transform.position.y + 3, 0).normalized * BearSpeed * 5;
            yield return null;
        }
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(transform.position.x, upY + 3);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamage damage = other.GetComponent<IDamage>();

        if (damage != null)
        {
            damage.Damage(attackDamage);
        }
    }
}
