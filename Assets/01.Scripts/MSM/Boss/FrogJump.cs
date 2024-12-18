using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogJump : MonoBehaviour
{
    [SerializeField] private Transform targetPoint; // ��ǥ ��ġ
    [SerializeField] private float firingAngle = 45.0f; // �߻� ����
    [SerializeField] private float speed = 5f; // �ӵ�
    private float gravity;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.gravityScale = 0;  // �߷� ��Ȱ��ȭ

        // ��ǥ�� ���� ��ġ ���� �Ÿ� ���
        float target_Distance = Vector2.Distance(transform.position, targetPoint.position);

        

        // �ʱ� �ӵ� ��� (���� �Ÿ��� �߻� ������ ���� �ӵ�)
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // ���� �ӵ�(Vx)�� ���� �ӵ�(Vy) ���
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad) * speed;
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad) * (speed / 1.5f);
        if (target_Distance < 10f)
        {
            Vy *= 2f;
        }
        // ���� �ð� ���
        float flightDuration = target_Distance / Vx;

        // ��ǥ �������� ���ϴ� ���� ���
        Vector2 direction = (targetPoint.position - transform.position).normalized;

        // ���� �ð� ���� �̵�
        float elapse_time = 0;
        while (elapse_time < flightDuration)
        {
            // ���� �� ���� �̵��� �ݿ��Ͽ� �̵� ���
            float xMovement = direction.x * Vx * Time.deltaTime;
            float yMovement = (Vy - (gravity * elapse_time)) * Time.deltaTime;

            // �̵�
            transform.Translate(xMovement, yMovement, 0);

            elapse_time += Time.deltaTime;
            yield return null;
        }

        rb.gravityScale = gravity * Vector2.Distance(targetPoint.position , transform.position);
    }
}
