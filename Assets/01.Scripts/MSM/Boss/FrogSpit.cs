using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FrogSpit : MonoBehaviour
{
    [SerializeField] GameObject spitPrefab;
    [SerializeField] float distance;
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Attack();
        }
    }
    public void Attack()
    {
        int angleIndex = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                float angle = angleIndex * 45f;
                GameObject spit = Instantiate(spitPrefab, transform.position + new Vector3(i * distance, j * distance), Quaternion.Euler(0, 0, angle));
                angleIndex++;
            }
        }
    }
}
