using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpiderSpit : MonoBehaviour
{
    [SerializeField] GameObject spitPrefab;
    [SerializeField] Transform spitTrans;
    [SerializeField] float distance;
    float[] angles = new float[] { 53 , -53 , 65.5f, -65.5f, -11.25f ,- 78f, 78f, 11.25f , -22.5f, 22.5f, -34f, 34f, 90, -90, 45, 0, -45 ,110,-110,100,-100 };
    public void Attack()
    {
        int rand = Random.Range(0, angles.Length);
        for (int i = 0; i < angles.Length; i++)
        {
            if(i == rand)
            {
                continue;
            }
            float angle = angles[i];
            GameObject spit = Instantiate(spitPrefab, spitTrans.position , Quaternion.Euler(0, 0, angle));
        }
    }
}
