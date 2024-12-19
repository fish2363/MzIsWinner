using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class Bat : MonoBehaviour
{
    [SerializeField]Transform targetTrans;
    [SerializeField]Vector2 distance;
    [SerializeField] float _timerMin , _timerMax;
    [SerializeField] private float speed;
    [SerializeField] private float checkerRadius;
    [SerializeField] LayerMask player;
    [SerializeField] SpriteRenderer eyeSprite;
    private Rigidbody2D rb;
    private bool once = true;
    float _timer;
    float _time;
    private bool end = false;
    [SerializeField]private bool isFind = false;
    private Animator animator;
    [SerializeField]
    private int attackDamage;
    private Light2D light;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _timer = Random.Range(_timerMin, _timerMax);
        light = GetComponentInChildren<Light2D>();
    }

    private void Start()
    {
        targetTrans = FindAnyObjectByType<Player>().transform;
    }

    private void Update()
    {
        if (isFind)
            _timer -= Time.deltaTime;
        if (!isFind)
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, checkerRadius, player);
            if (hit != null)
            {
                eyeSprite.DOFade(1, 0.2f);
                StartCoroutine(WaitRoutine());
                targetTrans = hit.transform;
            }
        }
        
    }

    private IEnumerator WaitRoutine()
    {   
        
        yield return new WaitForSeconds(1f);
        print("BatAppear");
        DOTween.To(() => light.intensity, x => light.intensity = x, 25, 0.5f);
        AnimationPlayer.Instance.PlayAnimaiton(animator, "BatAppear");
        yield return new WaitForSeconds(1f);
        animator.GetComponent<SpriteRenderer>().DOFade(1, 1);
       
        yield return new WaitForSeconds(1f);
        eyeSprite.gameObject.SetActive(false);
        isFind = true;
    }

    private void FixedUpdate()
    {
        if (isFind)
        {
            Vector3 moveDir;
            if(_timer <= 0&& once)
            {
                transform.DOKill();
                moveDir = targetTrans.position - transform.position;
                rb.velocity = moveDir.normalized * speed * 3;
                AnimationPlayer.Instance.PlayAnimaiton(animator, "BatAttack");
                once = false;
            }
            else if (once)
            {
                StopCoroutine(WaitRoutine());
                _time += Time.deltaTime;
                float X = Mathf.Sin(_time * speed) * distance.x;
                moveDir = new Vector3(X/2f, distance.y, 0f);
                transform.DOMove(targetTrans.position + moveDir,1f);
                print("BatFlying");
                AnimationPlayer.Instance.PlayAnimaiton(animator,"BatFlying");
                //transform.position = targetTrans.position + moveDir;
            }
            else if(end)
            {
                moveDir = new Vector2(-1,1);
                rb.velocity = moveDir.normalized * speed* 10;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        end = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamage damage = other.GetComponent<IDamage>();

        if (damage != null)
        {
            damage.Damage(attackDamage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkerRadius);
        Gizmos.color = Color.white;
    }
}
