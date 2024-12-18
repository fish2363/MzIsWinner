using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    private int attackDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamage damage = other.GetComponent<IDamage>();

        if (damage != null)
        {
            damage.Damage(attackDamage);
        }
    }
}
