using System.Collections;
using UnityEngine;

public class EnemyRangedWeapon : EnemyWeapon
{
    private void Start()
    {
        damage = 3;
    }

    private void OnEnable()
    {
        Coroutine attackCoroutine = StartCoroutine(AttackDelay());
    }

    IEnumerator AttackDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return new WaitUntil(() => canAttack);

            if (isBetterAttack)
                BetterAttack();
            else
                Attack();

            yield return wait;
        }
    }

    protected override void Attack()
    {
        GameObject bullet = ObjectPoolManager.instance.GetObject("Bullet");
        bullet.transform.position = transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.GetComponent<Bullet>().SetDirection(dir);
        bullet.GetComponent<Bullet>().SetDamage(damage);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    protected override void BetterAttack()
    {
        GameObject shot = ObjectPoolManager.instance.GetObject("Shot");
        shot.transform.position = transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        shot.GetComponent<Shot>().SetDirection(dir);
        shot.GetComponent<Shot>().SetDamage(damage * 4);
    }
}
