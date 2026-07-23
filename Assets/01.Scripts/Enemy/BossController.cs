using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    BossStateMachine stateMachine;

    [SerializeField] float maxHP;
    [SerializeField] float nowHP;

    [SerializeField] float detectRange;
    [SerializeField] float defenseRange;
    [SerializeField] float attackRange;

    [SerializeField] GameObject bossHPCanvas;

    public GameObject goldFish;
    public bool isDefense = false;
    public int attackCount = 0;

    float moveSpeed;
    float attackCoolTime = 1f;
    float attackTimer;
    float defenseCoolTime = 8f;
    float defenseTimer;

    Vector3 startPos;

    Transform target;
    
    SpriteRenderer sr;
    Animator animator;
    int isMove;

    EnemyWeapon enemyWeapon;

    private void OnEnable()
    {
        nowHP = maxHP;
    }

    void Start()
    {
        startPos = transform.position;

        moveSpeed = 4f;

        sr = GetComponent<SpriteRenderer>();

        enemyWeapon = GetComponent<EnemyWeapon>();
        target = GameObject.FindWithTag("Player").transform;

        stateMachine = new BossStateMachine(this);
        stateMachine.ChangeState(stateMachine.idleState);

        animator = GetComponent<Animator>();
        isMove = Animator.StringToHash("isMove");
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        defenseTimer += Time.deltaTime;

        stateMachine.Update();
    }

    public void ResetBoss()
    {
        transform.position = startPos;

        nowHP = maxHP;

        stateMachine.ChangeState(stateMachine.idleState);
        enemyWeapon.CanAttack(false);
    }

    public bool IsDetectPlayer()
    {
        return Vector2.Distance(transform.position, target.position) <= detectRange;
    }
    
    public bool IsDefenseRange()
    {
        return Vector2.Distance(transform.position, target.position) <= defenseRange;
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

    public void SetAttack(bool isBett)
    {
        enemyWeapon.SetAttackType(isBett);
    }

    public void Trace()
    {
        Debug.Log("Trace");

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

    public bool CanDefense()
    {
        return defenseTimer >= defenseCoolTime;
    }

    public void TakeDamage(float damage)
    {
        if (isDefense)
            return;

        nowHP -= damage;
        
        SetHPBar();

        animator.SetTrigger("damage");

        if (nowHP <= 0)
        {
            nowHP = 0;
            Die();
        }
    }

    void SetHPBar()
    {
        bossHPCanvas.transform.GetChild(1).GetComponent<Image>().fillAmount = nowHP / maxHP;
    }

    public void Defense()
    {
        defenseTimer = 0f;
        isDefense = true;
        PlayDefenseAnim();
    }

    void Die()
    {
        stateMachine.ChangeState(stateMachine.deadState);
        
        goldFish.SetActive(true);
        bossHPCanvas.SetActive(false);
    }

    public void PlayMoveAnim(bool value)
    {
        animator.SetBool(isMove, value);
    }

    public void PlayNormalAttackAnim()
    {
        animator.SetTrigger("attack1");
    }

    public void PlayBetterAttackAnim()
    {
        animator.SetTrigger("attack2");
    }

    public void PlayDefenseAnim()
    {
        enemyWeapon.CanAttack(false);

        animator.SetTrigger("defense");
    }

    public void PlayDeadAnim()
    {
        animator.SetTrigger("dead");
    }

    public void EndDefense()
    {
        isDefense = false;
    }

    public void DeadEnd()
    {
        gameObject.SetActive(false);
    }
}