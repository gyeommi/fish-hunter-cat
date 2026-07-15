using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyStateMachine stateMachine;

    [SerializeField] float maxHP;
    [SerializeField] float nowHP;

    [SerializeField] float detectRange;
    [SerializeField] float attackRange;

    float moveSpeed;
    float attackCoolTime = 1f;
    float attackTimer;

    Transform target;
    SpriteRenderer sr;

    EnemyWeapon enemyWeapon;

    Vector3 startPos;

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
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        stateMachine.Update();
    }

    public void ResetEnemy()
    {
        transform.position = startPos;
        
        nowHP = maxHP;

        gameObject.SetActive(true);

        stateMachine.ChangeState(stateMachine.idleState);
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

    public void Trace()
    {
        enemyWeapon.CanAttack(false);

        sr.flipX = CheckFlip();
        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    Vector2 GetDirection()
    {
        return target.position - transform.position;
    }

    bool CheckFlip()
    {
        return transform.position.x > target.position.x ? true : false;
    }

    public void TakeDamage(int damage)
    {
        nowHP -= damage;
        if (nowHP <= 0)
        {
            nowHP = 0;
            Die();
        }
    }
    
    void Die()
    {
        StageManager.instance.RemoveEnemy(gameObject, startPos);
    }
}
