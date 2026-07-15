using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyStateMachine stateMachine;

    [SerializeField] float maxHP;
    [SerializeField] float nowHP;

    [SerializeField] float detectRange;
    [SerializeField] float attackRange;

    float moveSpeed;

    Transform target;
    SpriteRenderer sr;

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
        target = GameObject.FindWithTag("Player").transform;

        stateMachine = new EnemyStateMachine(this);
        stateMachine.ChangeState(stateMachine.idleState);
    }

    void Update()
    {
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
        //공격 -> 플레이어 방향으로 공격,,
    }

    public void Trace()
    {
        //추적 -> 플레이어 방향으로 이동..
        sr.flipX = CheckFlip();

        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
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
