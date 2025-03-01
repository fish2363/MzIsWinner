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
    [SerializeField]
    private ParticleSystem particle;
    public bool isRest;
    private bool isClear;

    [SerializeField]
    private GameObject aimTarget;

    private void Update()
    {
        if(isRest)
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, checkerRadius, whatIsPlayer);

            if (hit != null && !isClear)
            {
                hit.GetComponent<Player>().RigidCompo.velocity = Vector2.zero;
                hit.GetComponent<Player>().isAttack = false;
                Death();
            }
        }
        aimTarget.SetActive(isRest);
    }

    private void Death()
    {
        print("Ŭ����");
        isClear = true;
        //particle.Play();
        ScreenShakeManager.Instance.SuccessAttack();
        GetComponentInParent<IBoss>().DeathBoss();
        AnimationPlayer.Instance.PlayAnimaiton(GetComponentInParent<Animator>(),"Death");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkerRadius);
        Gizmos.color = Color.red;
    }
}
