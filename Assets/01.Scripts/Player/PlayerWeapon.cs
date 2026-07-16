using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerWeapon : MonoBehaviour
{
    protected Camera camera;

    [SerializeField] protected float damage;
    [SerializeField] private float delay = 1f;
    [SerializeField] protected bool canAttack;

    WaitForSeconds wait;
    Coroutine attackCoroutine;

    private void OnEnable()
    {
        canAttack = true;
        delay = 1f;
        wait = new WaitForSeconds(delay);

        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        attackCoroutine = StartCoroutine(AttackDelay());
    }

    private void OnDisable()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    protected virtual void Start()
    {
        camera = Camera.main;
    }

    protected abstract void Attack();

    protected void LookMouse()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Vector3 worldPos = camera.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        Vector2 dir = worldPos - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    IEnumerator AttackDelay()
    {
        while (true)
        {
            yield return new WaitWhile(() => canAttack);
            yield return wait;
            canAttack = true;
        }
    }
}
