using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpiderDown : MonoBehaviour
{
    [SerializeField]private float upY;
    [SerializeField]private float downY;
    [SerializeField] private float y;
    [SerializeField] float timeRandMin;
    [SerializeField] float timeRandMax;
    [SerializeField] float SpiderSpeed;
    Animator anim;
    Rigidbody2D rb;
    private Transform target;
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        transform.parent.parent.position = new Vector3(0, y, 0);
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
            if (Mathf.Abs(transform.parent.parent.position.x - target.position.x) > 0.1f)
            {
                rb.velocity = new Vector3(target.position.x - transform.parent.parent.position.x, 0, 0).normalized * SpiderSpeed;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
            yield return null;
        }


        while (transform.parent.parent.position.y < upY)
        {
            rb.velocity = new Vector3(0, upY - transform.parent.parent.position.y, 0).normalized * SpiderSpeed * 2f;
            yield return null;
        }
        transform.parent.parent.position = new Vector3(transform.parent.parent.position.x, upY);


        AnimationPlayer.Instance.PlayAnimaiton(anim, "SpiderAttack");
        while (transform.parent.parent.position.y > downY)
        {
            rb.velocity = new Vector3(0, downY - transform.parent.parent.position.y, 0).normalized * SpiderSpeed * 5;
            yield return null;
        }
        SoundManager.Instance.ChangeMainStageVolume("SpiderAttack", true, ISOund.SFX);
        transform.parent.parent.position = new Vector3(transform.parent.parent.position.x, downY);

        while (transform.parent.parent.position.y < y)
        {
            rb.velocity = new Vector3(0.1f - transform.parent.parent.position.x, y - transform.parent.parent.position.y, 0).normalized * SpiderSpeed * 5;
            yield return null;
        }
        rb.velocity = Vector3.zero;
        transform.parent.parent.position = new Vector3(0,y);

    }


}
