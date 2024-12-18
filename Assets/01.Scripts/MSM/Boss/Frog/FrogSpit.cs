using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FrogSpit : MonoBehaviour
{
    [SerializeField]
    private Frog frog;

    [SerializeField] GameObject spitPrefab;
    [SerializeField] float distance;
    float[] angles = new float[] { 135, 180, -135, 90 , -90, 45, 0, -45 };
    public void Attack()
    {
        AnimationPlayer.Instance.PlayAnimaiton(frog.frogAnimator, "FrogMouseOpen");
        int angleIndex = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                float angle = angles[angleIndex];
                GameObject spit = Instantiate(spitPrefab, transform.parent.position + new Vector3(j * distance, i * distance), Quaternion.Euler(0, 0, angle));
                angleIndex++;
            }
        }
    }
}
