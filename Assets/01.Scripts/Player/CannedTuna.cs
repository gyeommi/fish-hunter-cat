using UnityEngine;

public class CannedTuna : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    
    Rigidbody2D rb;
    
    float timer;
    float lifeTime = 3f;

    float damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        if (timer >= lifeTime)
        {
            ReturnPool();
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        rb.linearVelocity = transform.right * speed;
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            ReturnPool();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            collision.gameObject.GetComponent<BossController>().TakeDamage(damage);
            ReturnPool();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ReturnPool();
        }
    }
    
    void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("CannedTuna", this.gameObject);
    }
}
