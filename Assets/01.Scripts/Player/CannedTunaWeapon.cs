using UnityEngine;
using UnityEngine.InputSystem;

public class CannedTunaWeapon : PlayerWeapon
{
    [SerializeField] Transform firePosition;

    [SerializeField] GameObject can;

    protected override void Start()
    {
        base.Start();
        damage = 5;
    }

    private void Update()
    {
        LookMouse();
        Attack();
    }

    protected override void Attack()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame && canAttack)
        {
            GameObject tuna = ObjectPoolManager.instance.GetObject("CannedTuna");

            tuna.transform.position = firePosition.position;
            tuna.transform.rotation = transform.rotation;
            tuna.GetComponent<CannedTuna>().SetDamage(damage);
            canAttack = false;
        }
    }
}
