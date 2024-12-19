using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BearScratch : MonoBehaviour
{
    [SerializeField] Transform Y;
    [SerializeField] Transform DownY;
    [SerializeField]SpriteRenderer warning;
    private float y;
    private float downY;
    Rigidbody2D rb;
    Transform target;
    [SerializeField] float BearSpeed;
    private void Awake()
    {
        y = Y.position.y;
        downY = DownY.position.y;
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(0, downY, 0);
    }
    public void TargetSet(Transform targetTrans)
    {
        target = targetTrans;
    }
    public IEnumerator Attack()
    {
        Color color = warning.color;
        for (int i = 0; i < 3; i++)
        {
            transform.position = new Vector3(target.position.x, transform.position.y);
            warning.transform.position = new Vector3(target.position.x, warning.transform.position.y);
            warning.DOFade(0.5f, 1f);
            yield return new WaitForSeconds(1);
            warning.color = color;
            while (Mathf.Abs(transform.position.y - y) > 0.1f)
            {
                rb.velocity = new Vector3(0, y - transform.position.y, 0).normalized * BearSpeed;
                yield return null;
            }
            while (transform.position.y > downY)
            {
                rb.velocity = new Vector3(0, downY - transform.position.y, 0).normalized * BearSpeed;
                yield return null;
            }
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, downY);
        }
        
    }
}
