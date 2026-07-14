using UnityEngine;

public class CannedTuna : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    
    Rigidbody2D rb;
    
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

    public void SetDamage(int dmg)
    {
        damage = dmg;
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
    }
}
