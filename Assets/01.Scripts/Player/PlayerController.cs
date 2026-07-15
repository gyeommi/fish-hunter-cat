using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private SpriteRenderer sr;

    float speed = 3f;
    float jumpPower = 7f;

    float dir;
    bool isGround;
    int jumpCount;
    int jumpCountMax;

    float dashPower;
    float dashCoolTime;
    bool canDash;
    bool isDashing;

    Animator animator;

    int isMove;
    int isJump;
    int isDash;
    int damageHash;

    [SerializeField] LayerMask groundLayer;

    [SerializeField] float nowHP;
    [SerializeField] int nowLife;
    float maxHP;
    int maxLife;

    [SerializeField] PlayerHealth health;

    [SerializeField] Transform respawnPoint;

    private void OnEnable()
    {
        nowHP = 20;
        maxHP = 20;
        nowLife = 3;
        maxLife = 3;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        dashPower = 15f;
        dashCoolTime = 1f;
        canDash = true;
        isDashing = false;

        jumpCount = 0;
        jumpCountMax = 2;

        animator = GetComponent<Animator>();
        isMove = Animator.StringToHash("isMove");
        isJump = Animator.StringToHash("isJump");
        isDash = Animator.StringToHash("isDash");
        damageHash = Animator.StringToHash("damage");
    }

    private void Update()
    {
        dir = 0;
        if (Keyboard.current.aKey.isPressed)
            dir += -1;
        if (Keyboard.current.dKey.isPressed)
            dir += 1;

        GroundCheck();

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }

        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            Dash();
        }

        if (dir == 0)
        {
            animator.SetBool(isMove, false);
        }
        else
        {
            animator.SetBool(isMove, true);
        }

        animator.SetBool(isJump, !isGround);
    }

    private void FixedUpdate()
    {
        if (isDashing)
            return;

        if (dir != 0)
        {
            if (dir > 0)
                sr.flipX = false;
            else
                sr.flipX = true;
        }
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
    }

    public void TakeDamage(float damage)
    {
        nowHP -= damage;
        animator.SetTrigger(damageHash);
        health.SetHPGauge(nowHP / maxHP);
        if (nowHP <= 0)
        {
            nowHP = 0;
            Die();
        }
    }

    public void ResetPlayer()
    {
        nowHP = maxHP;
        nowLife = maxLife;
        health.SetLife();
        health.SetHPGauge(nowHP / maxHP);
        transform.position = new Vector3(0, 0, 0);
    }

    public void Die()
    {
        if (nowLife <= 0)
        {
            StageManager.instance.GameOver();
            nowLife = 0;
        }
        nowLife--;
        health.DecreaseLife(nowLife);
        
        nowHP = maxHP;
        health.SetHPGauge(nowHP / maxHP);

        Respawn();
    }
    
    void Respawn()
    {
        transform.position = respawnPoint.position;
        StageManager.instance.ResetEnemy();
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.3f, Vector2.down, 0.8f, groundLayer);

        isGround = hit.collider == null ? false : true;

        if (isGround)
            jumpCount = 0;
    }

    private void Jump()
    {
        if (jumpCount >= jumpCountMax)
            return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);

        if (isGround)
            jumpCount++;
        else
            jumpCount += 2;
    }

    private void Dash()
    {
        if (!canDash || isDashing)
            return;

        canDash = false;
        isDashing = true;

        float direction = sr.flipX ? -1f : 1f;

        rb.linearVelocity = new Vector2(direction * dashPower, 0);

        animator.SetBool(isDash, true);

        StartCoroutine(DashCoolTime());
    }

    IEnumerator DashCoolTime()
    {
        yield return new WaitForSeconds(0.2f);

        isDashing = false;
        animator.SetBool(isDash, false);

        yield return new WaitForSeconds(dashCoolTime);
        
        canDash = true;
    }
}
