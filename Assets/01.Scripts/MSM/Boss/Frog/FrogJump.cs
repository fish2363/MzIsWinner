using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogJump : MonoBehaviour
{
    private Transform targetPoint; // 목표 위치
    [SerializeField] private float firingAngle = 45.0f;
    [SerializeField] private float speed = 5f;
    private float gravity;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }
    public void TargetSet(Transform target)
    {
        targetPoint = target;
    }
    private void Start()
    {
        gravity = rb.gravityScale;
    }

    public void Attack()
    {
        StartCoroutine(SimulateProjectile());
    }
    private IEnumerator SimulateProjectile()
    {
        rb.gravityScale = 0;

        // 목표와 현재 위치 간의 거리 계산
        float target_Distance = Vector2.Distance(transform.parent.position, targetPoint.position);

        

        // 초기 속도 계산 (수평 거리와 발사 각도에 따른 속도)
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // 수평 속도(Vx)와 수직 속도(Vy) 계산
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad) * speed;
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad) * (speed / 1.65f);
        if (target_Distance < 10f)
        {
            Vy *= 2f;
        }
        // 비행 시간 계산
        float flightDuration = target_Distance / Vx;

        // 목표 지점으로 향하는 방향 계산
        Vector2 direction = (targetPoint.position - transform.parent.position).normalized;

        // 비행 시간 동안 이동
        float elapse_time = 0;
        while (elapse_time < flightDuration)
        {
            // 수평 및 수직 이동을 반영하여 이동 계산
            float xMovement = direction.x * Vx * Time.deltaTime;
            float yMovement = (Vy - (gravity * elapse_time)) * Time.deltaTime;

            // 이동
            transform.parent.Translate(xMovement, yMovement, 0);

            elapse_time += Time.deltaTime;
            yield return null;
        }

        rb.gravityScale = gravity * Vector2.Distance(targetPoint.position , transform.parent.position);
    }
}
