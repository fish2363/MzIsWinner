using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateEnum
{
    Idle,
    Move,
    Attack,
    Skill,
    Death
}

public enum AnimationType
{
    PlayerIdle,
    PlayerAttackReady,
    PlayerMove,
    PlayerAttacking,
}

public interface IDamage
{
    void Damage(int damage);
}


public class Player : MonoBehaviour,IDamage
{
    [field: SerializeField] public InputReader inputReader { get; private set; }

    [Header("이동속도")]
    public float moveSpeed;

    public Rigidbody2D RigidCompo { get; private set; }
    public Action OnnAttack;

    [SerializeField]
    private Animator[] animators;
    [field: SerializeField] public Animator AnimatorCompo { get; private set; }
    public SpriteRenderer SpriteCompo { get; private set; }

    private Dictionary<StateEnum, State> stateDictionary = new Dictionary<StateEnum, State>();
    private StateEnum currentEnum;

    [Header("공격 딜레이")]
    [SerializeField]
    private float attackDelay;

    [field: SerializeField] public float DashPower { get; private set; }

    [HideInInspector]
    public AttackPoint attackPoint;

    public Vector2 mouseDir { get; private set; }

    public bool isStopMove { get; set; }
    public bool isAttack { get; set; }

    public CharacterSO currentChracter;
    public BeeType currentBee;

    public LayerMask whatIsEntity;

    [Header("최대 체력")]
    public int MaxHp = 3;
    [Header("현재 체력")]
    public int CurrentHp;
    public float checkerRadius =1f;
    public bool isUndead { get; set; }
    public bool isShield { get; set; }

    public ParticleSystem shieldParticle;
    public ParticleSystem healingParticle;
    public bool isSkillLock;
    public bool isAttackLock;
    public bool isTutorial;
    public bool isSkillUsing;
    public Vector2 mouseDirect;

    private float maxaDashTime = 0.2f;
    private float dashTime;

    [SerializeField]
    private CinemachineVirtualCamera cinemachine;

    private void Awake()
    {
        RigidCompo = GetComponent<Rigidbody2D>();
        attackPoint = GetComponentInChildren<AttackPoint>();

        foreach (StateEnum enumState in Enum.GetValues(typeof(StateEnum)))
        {
            Type t = Type.GetType($"{enumState}State");
            State state = Activator.CreateInstance(t, new object[] { this }) as State;
            stateDictionary.Add(enumState, state);
        }

        inputReader.OnAttackEvent += HandleAttackEvent;
        inputReader.OnAttackingEvent += HandleAttackingEvent;
        inputReader.OnDashEvent += HandleDashEvent;
    }

    private void Start()
    {
        //currentChracter = SpawnManager.Instance.characterList.characters[SpawnManager.Instance.idx];
        //cinemachine.Follow = transform;
        AnimatorCompo = animators[currentChracter.beeIdx];
        AnimatorCompo.gameObject.SetActive(true);
        SpriteCompo = AnimatorCompo.GetComponent<SpriteRenderer>();
        moveSpeed = currentChracter.moveSpeed;
        MaxHp = currentChracter.maxHp;
        CurrentHp= MaxHp;
        currentBee = currentChracter.beeType;
        if(!isTutorial)
        HpManager.Instance.SetActiveHp();
        ChangeState(StateEnum.Idle);
    }

    
    private IEnumerator SkillCoolTime()
    {
        yield return new WaitForSeconds(currentChracter.skillCool);
        //SpawnManager.Instance.skillUI.DOColor(Color.white, 0.2f);
        isSkillLock = false;
    }

    private void HandleAttackingEvent(bool isOnoff)
    {
        if(!isAttackLock)
        {
            if (isOnoff)
                attackPoint.FadeInAttackPoint();
            else
                attackPoint.FadeOutAttackPoint();
        }
    }

    private void Update()
    {
        stateDictionary[currentEnum].StateUpdate();

        GetWorldMousePosition();
        if(isSkillUsing)
        {
            dashTime += Time.deltaTime;
            if(currentBee == BeeType.Default || currentBee == BeeType.Ninja)
                RigidCompo.velocity = mouseDirect.normalized * DashPower;

            if (dashTime >= maxaDashTime)
            {
                dashTime = 0f;
                SpriteCompo.DOFade(1f, 0.2f);
                SpriteCompo.DOColor(Color.white, 0.2f);
                moveSpeed = currentChracter.moveSpeed;
                isUndead = false;
                ChangeState(StateEnum.Idle);
                isSkillUsing = false;
            }
        }
    }

    public Vector3 GetWorldMousePosition()
    {
        mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);//스크린을 월드 좌표계로 바꾼다
        mouseDir.Normalize();
        return mouseDir;
    }

    public void FilpWeapon(bool value)
    {
        int flip = (value ? -1 : 1);

        if(flip == 1)
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        else
            GetComponentInChildren<SpriteRenderer>().flipX = true;

    }

    public void AttackWait()
    {
        StartCoroutine(AttackWaitRoutine());
    }

    public IEnumerator AttackWaitRoutine()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttack = true;
    }

    private void FixedUpdate()
    {
        stateDictionary[currentEnum].StateFixedUpdate();
    }

    private void HandleAttackEvent()
    {
        if (!isAttackLock)
        {
            if (!isStopMove)
            {
                OnnAttack?.Invoke();
                ChangeState(StateEnum.Attack);
                isStopMove = true;
            }
        }
    }

    
    public void ChangeState(StateEnum newEnum)
    {
        print($"{currentEnum}에서 {newEnum}로");
        stateDictionary[currentEnum].Exit();
        currentEnum = newEnum;
        stateDictionary[currentEnum].Enter();
    }

    public void Damage(int damage)
    {
        if(isShield)
        {
            StopCoroutine(ShieldRoutine());
            shieldParticle.Clear();
            isStopMove = false;
            isUndead = false;
            ChangeState(StateEnum.Idle);
        }

        if (!isUndead)
        {
            SpriteCompo.DOColor(Color.red, 0.1f);
            SpriteCompo.DOColor(Color.white, 0.1f).SetDelay(0.5f);
            if (!isTutorial)
            {
                CurrentHp -= damage;
                ScreenShakeManager.Instance.ScreenShake(20f, true, 0.2f, true, 0.5f);
            }
            if (!isTutorial)
                HpManager.Instance.HpChange(CurrentHp);
            if (CurrentHp == 0)
                Death();
        }
    }

    public void Death()
    {
        ChangeState(StateEnum.Death);
        ScreenShakeManager.Instance.SuccessAttack();
        //공격 잘못 박거나 체력 0
    }

    public void Shield()
    {
        StartCoroutine(ShieldRoutine());
    }

    private IEnumerator ShieldRoutine()
    {
        yield return new WaitForSeconds(1f);
        isStopMove = false;
        isUndead = false;
        shieldParticle.Clear();
        yield return new WaitForSeconds(0.3f);
        ChangeState(StateEnum.Idle);
    }
    public void DeathWait()
    {
        StartCoroutine(DeathWaitRoutine());
    }
    private IEnumerator DeathWaitRoutine()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        GameManager.Instance.RestartScene();
        SpawnManager.Instance.ReStart();

        //부활인데 아직 ㄴㄴ
    }
    private void OnDestroy()
    {
        inputReader.OnAttackEvent -= HandleAttackEvent;
        inputReader.OnAttackingEvent -= HandleAttackingEvent;
        inputReader.OnDashEvent -= HandleDashEvent;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkerRadius);
        Gizmos.color = Color.white;
    }










    private void HandleDashEvent()
    {
        if (!isSkillLock)
        {
            if (isAttack) return;

            ChangeState(StateEnum.Skill);
            isSkillLock = true;
            StartCoroutine(SkillCoolTime());
        }
    }


}
