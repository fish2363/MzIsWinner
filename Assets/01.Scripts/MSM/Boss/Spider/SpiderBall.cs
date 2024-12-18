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
        GameObject spit = Instantiate(ballPrefab , BallTrans.position , new Quaternion(0,0,Random.Range(0.0f,360.1f) * Mathf.Deg2Rad, transform.rotation.w));
    }
}
