using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Boundery : MonoBehaviour
{
    private void Awake()
    {
        FindAnyObjectByType<CinemachineConfiner2D>().m_BoundingShape2D = gameObject.GetComponent<PolygonCollider2D>();
    }
}
