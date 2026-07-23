using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    Rigidbody2D rb;

    Vector2 dir;

    float timer;
    float lifeTime = 3f;

    int damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    private void OnDisable()
    {
        ReturnPool();
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

        rb.linearVelocity = dir * speed;

        rb.MoveRotation(rb.rotation + -180f * Time.fixedDeltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        dir = direction;
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            ReturnPool();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ReturnPool();
        }
    }

    void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("Shot", this.gameObject);
        transform.rotation = Quaternion.identity;
    }
}
