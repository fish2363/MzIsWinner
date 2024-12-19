using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaknessPoint : MonoBehaviour
{
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private float checkerRadius;

    public bool isRest;

    [SerializeField]
    private GameObject aimTarget;

    private void Update()
    {
        if(isRest)
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, checkerRadius, whatIsPlayer);

            if (hit != null)
            {
                Death();
            }
        }
        aimTarget.SetActive(isRest);
    }

    private void Death()
    {
        print("Å¬¸®¾î");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkerRadius);
        Gizmos.color = Color.red;
    }
}
