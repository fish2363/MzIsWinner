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
    [SerializeField] int spitMany;
    public void Attack()
    {
        int rand = Random.Range(0, spitMany);
        for (int i = 0; i < spitMany; i++)
        {
            if(i == rand)
            {
                continue;
            }
            float angle = 360f / spitMany * i;
            GameObject spit = Instantiate(spitPrefab, spitTrans.position , Quaternion.Euler(0, 0, angle));
        }
    }
}
