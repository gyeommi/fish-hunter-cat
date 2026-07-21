using DG.Tweening;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyStateMachine stateMachine;

    [SerializeField] float maxHP;
    [SerializeField] float nowHP;

    [SerializeField] float detectRange;
    [SerializeField] float attackRange;

    [SerializeField] bool canFly;

    float moveSpeed;
    float attackCoolTime = 1f;
    float attackTimer;

    Transform target;
    SpriteRenderer sr;
    
    EnemyWeapon enemyWeapon;

    Tween idleTween;

    Vector3 startPos;

    Animator animator;

    private void OnEnable()
    {
        nowHP = maxHP;
    }

    void Start()
    {
        startPos = transform.position;

        StageManager.instance.RegisterEnemy(this);
        
        moveSpeed = 3f;

        sr = GetComponent<SpriteRenderer>();

        enemyWeapon = GetComponent<EnemyWeapon>();
        target = GameObject.FindWithTag("Player").transform;

        stateMachine = new EnemyStateMachine(this);
        stateMachine.ChangeState(stateMachine.idleState);

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        stateMachine.Update();
    }

    public void ResetEnemy()
    {
        idleTween?.Kill();
        idleTween = null;
        transform.position = startPos;

        transform.position = startPos;
        
        nowHP = maxHP;

        gameObject.SetActive(true);

        stateMachine.ChangeState(stateMachine.idleState);
        enemyWeapon.CanAttack(false);
    }

    public bool IsDetectPlayer()
    {
        return Vector2.Distance(transform.position, target.position) <= detectRange;
    }

    public bool IsAttackRange()
    {
        return Vector2.Distance(transform.position, target.position) <= attackRange;
    }

    public void Attack()
    {
        if (attackTimer < attackCoolTime)
            return;

        attackTimer = 0f;

        float distance = Vector3.Distance(transform.position, target.position);
        
        enemyWeapon.SetDirection(GetDirection());
        enemyWeapon.SetDistance(distance);
        enemyWeapon.CanAttack(true);
    }

    public void Idle()
    {
        if (idleTween != null && idleTween.IsActive())
            return;

        idleTween = transform.DOMoveX(startPos.x + 2f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    public void Trace()
    {
        idleTween?.Kill();
        idleTween = null;

        enemyWeapon.CanAttack(false);

        sr.flipX = CheckFlip();
        Move();
    }

    void Move()
    {
        if (canFly)
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, transform.position, moveSpeed * Time.deltaTime);
    }

    Vector2 GetDirection()
    {
        return target.position - transform.position;
    }

    bool CheckFlip()
    {
        return transform.position.x > target.position.x ? true : false;
    }

    public void TakeDamage(float damage)
    {
        nowHP -= damage;

        sr.DOKill();

        sr.color = Color.white;
        sr.DOColor(Color.red, 0.2f).OnComplete(() => { sr.DOColor(Color.white, 0.2f); });

        if (nowHP <= 0)
        {
            nowHP = 0;
            Die();
        }
    }
    
    void Die()
    {
        stateMachine.ChangeState(stateMachine.deadState);
    }

    public void PlayDeadAnim()
    {
        idleTween?.Kill();
        idleTween = null;

        enemyWeapon.CanAttack(false);

        animator.SetTrigger("Dead");
    }

    public void DeadEnd()
    {
        StageManager.instance.RemoveEnemy(gameObject, startPos);
    }
}
