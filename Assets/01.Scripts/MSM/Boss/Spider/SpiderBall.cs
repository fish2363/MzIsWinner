using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpiderBall : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform BallTrans;
    public void Attack()
    {
        GameObject spit = Instantiate(ballPrefab , BallTrans.position , new Quaternion(0,0,Random.Range(-85.0f,85.1f), transform.rotation.w));
    }
}
