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

    [SerializeField]
    private int attackDamage;

    private void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Wall")|| other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            IDamage damage = other.GetComponent<IDamage>();

            if (damage != null)
            {
                damage.Damage(attackDamage);
            }
            Destroy(gameObject);
        }
    }
}
