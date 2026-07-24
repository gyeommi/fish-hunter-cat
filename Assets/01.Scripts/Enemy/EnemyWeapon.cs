using UnityEngine;

public abstract class EnemyWeapon : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected int range;
    [SerializeField] protected float delay;
    [SerializeField] protected bool isBetterAttack;

    protected float distance;
    protected Vector2 dir;
    protected bool canAttack;

    public void SetDistance(float dis)
    {
        distance = dis;
    }

    public void SetDirection(Vector2 direction)
    {
        dir = direction;
    }

    public void SetAttackType(bool isBetter)
    {
        isBetterAttack = isBetter;
    }

    public void CanAttack(bool canAtt)
    {
        canAttack = canAtt;
    }

    protected abstract void Attack();

    protected abstract void BetterAttack();
}
