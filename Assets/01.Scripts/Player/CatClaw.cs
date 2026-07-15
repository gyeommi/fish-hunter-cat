using UnityEngine;
using UnityEngine.InputSystem;

public class CatClaw : PlayerWeapon
{
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRadius = 1f;
    [SerializeField] LayerMask monsterLayer;

    protected override void Start()
    {
        base.Start();
        damage = 3;
    }

    void Update()
    {
        LookMouse();
        Attack();
    }

    protected override void Attack()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack)
        {
            canAttack = false;

            Collider2D[] monsters = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, monsterLayer);

            foreach (Collider2D monster in monsters)
            {
                monster.GetComponent<EnemyController>()?.TakeDamage(damage);
            }
            //애니메이션 실행
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}