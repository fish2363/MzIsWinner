using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    
    [SerializeField] private Vector2 _checkerSize;

    [SerializeField] private LayerMask _whatIsGround;


    public bool CheckGround()
    {
        Vector3 position = transform.position;
        Collider2D collider = Physics2D.OverlapBox(position, _checkerSize, 0, _whatIsGround);
        return collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _checkerSize);
        Gizmos.color = Color.white;
    }
}
