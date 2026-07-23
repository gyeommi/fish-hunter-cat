using UnityEngine;
using UnityEngine.InputSystem;

public class CatClaw : PlayerWeapon
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRadius = 0.1f;
    [SerializeField] LayerMask monsterLayer;

    int attackHash;

    protected override void Start()
    {
        base.Start();
        damage = PlayerStats.instance.SetMeleeDamage();

        attackHash = Animator.StringToHash("attack");
    }

    void Update()
    {
        LookMouse();
    }

    public override void Attack()
    {
        if (canAttack)
        {
            canAttack = false;

            BossController boss = FindFirstObjectByType<BossController>();

            if (boss != null && boss.gameObject.activeInHierarchy)
            {
                boss.GetComponent<BossController>()?.TakeDamage(damage);
            }
            else
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, monsterLayer);

                foreach (Collider2D enemy in enemies)
                {
                    enemy.GetComponent<EnemyController>()?.TakeDamage(damage);
                }
            }
            //애니메이션 실행
            playerAnimator.SetTrigger(attackHash);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}