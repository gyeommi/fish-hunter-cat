using UnityEngine;
using UnityEngine.InputSystem;

public class CannedTunaWeapon : PlayerWeapon
{
    [SerializeField] Transform firePosition;

    [SerializeField] GameObject can;

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
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
            tuna.GetComponent<CannedTuna>().SetDamage(5);
            canAttack = false;
        }
    }
}
