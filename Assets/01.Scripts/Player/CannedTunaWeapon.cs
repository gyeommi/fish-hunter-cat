using UnityEngine;
using UnityEngine.InputSystem;

public class CannedTunaWeapon : PlayerWeapon
{
    [SerializeField] Transform firePosition;

    [SerializeField] GameObject can;

    protected override void Start()
    {
        base.Start();
        damage = PlayerStats.instance.SetRangedDamage();
    }

    private void Update()
    {
        LookMouse();
    }

    public override void Attack()
    {
        if (canAttack)
        {
            SoundManager.instance.PlaySFX(SFXType.CannedTuna);

            GameObject tuna = ObjectPoolManager.instance.GetObject("CannedTuna");

            tuna.transform.position = firePosition.position;
            tuna.transform.rotation = transform.rotation;
            tuna.GetComponent<CannedTuna>().SetDamage(damage);
            canAttack = false;
        }
    }
}
