using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpiderDown : MonoBehaviour
{
    [SerializeField] Transform Y;
    [SerializeField] Transform UpY;
    [SerializeField] Transform DownY;
    private float upY;
    private float downY;
    private float y;
    [SerializeField] float timeRandMin;
    [SerializeField] float timeRandMax;
    [SerializeField] float SpiderSpeed;
    Animator anim;
    Rigidbody2D rb;
    private Transform target;
    private void Awake()
    {
        y = Y.position.y;
        upY = UpY.position.y;
        downY = DownY.position.y;
        rb = GetComponentInParent<Rigidbody2D>();
        transform.parent.position = new Vector3(0, y, 0);
    }
    public void SetTarget(Transform targetTrans)
    {
        target = targetTrans;
    }
    public void SetAnimator(Animator animatorSetting)
    {
        anim = animatorSetting;
    }
    public IEnumerator Attack()
    {
        float timer = Random.Range(timeRandMin, timeRandMax);
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (Mathf.Abs(transform.parent.position.x - target.position.x) > 0.1f)
            {
                rb.velocity = new Vector3(target.position.x - transform.parent.position.x, 0, 0).normalized * SpiderSpeed;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
            yield return null;
        }


        while (transform.parent.position.y < upY)
        {
            rb.velocity = new Vector3(0, upY - transform.parent.position.y, 0).normalized * SpiderSpeed * 2f;
            yield return null;
        }
        transform.parent.position = new Vector3(transform.parent.position.x, upY);


        AnimationPlayer.Instance.PlayAnimaiton(anim, "SpiderAttack");
        while (transform.parent.position.y > downY)
        {
            rb.velocity = new Vector3(0, downY - transform.parent.position.y, 0).normalized * SpiderSpeed * 5;
            yield return null;
        }
        transform.parent.position = new Vector3(transform.parent.position.x, downY);

        while (transform.parent.position.y < y)
        {
            rb.velocity = new Vector3(0.1f - transform.parent.position.x, y - transform.parent.position.y, 0).normalized * SpiderSpeed * 5;
            yield return null;
        }
        rb.velocity = Vector3.zero;
        transform.parent.position = new Vector3(0,y);

    }
}
