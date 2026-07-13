using UnityEngine;

public class CannedTuna : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    float lifeTime;

    float timer;

    Rigidbody2D rb;
    Vector2 dir;
    int damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lifeTime = 3f;
        timer = 0f;
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    public void SetDirection(Vector2 direction)
    {
        dir = direction;
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //몬스터 데미지 입히기
            //collision.gameObject.GetComponent<MonsterController>().TakeDamage(damage);
            ReturnPool();
        }
    }
    
    void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("CannedTuna", this.gameObject);
        transform.rotation = Quaternion.identity;
    }
}
