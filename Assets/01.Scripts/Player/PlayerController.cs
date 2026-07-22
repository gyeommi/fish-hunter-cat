using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    PlayerStateMachine stateMachine;

    CatClaw catClaw;
    CannedTunaWeapon cannedTunaW;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public float dir;
    public bool isGround;

    private bool canDash = true;
    public bool isDashing = false;

    public Animator animator;

    int isMove;
    int isJump;
    int isDash;
    int damageHash;
    int attackHash;

    [SerializeField] LayerMask groundLayer;

    [SerializeField] Transform respawnPoint;
    [SerializeField] GameObject playerLight;

    public bool jumpPressed;
    public bool dashPressed;
    public bool meleeAttackPressed;
    public bool rangedAttackPressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        catClaw = GetComponentInChildren<CatClaw>();
        cannedTunaW = GetComponentInChildren<CannedTunaWeapon>();

        stateMachine = new PlayerStateMachine(this);
        stateMachine.ChangeState(stateMachine.idleState);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        isMove = Animator.StringToHash("isMove");
        isJump = Animator.StringToHash("isJump");
        isDash = Animator.StringToHash("isDash");
        damageHash = Animator.StringToHash("damage");
        attackHash = Animator.StringToHash("attack");
    }

    private void Update()
    {
        dir = 0;
        if (Keyboard.current.aKey.isPressed)
            dir += -1;
        if (Keyboard.current.dKey.isPressed)
            dir += 1;

        jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        dashPressed = Keyboard.current.leftShiftKey.wasPressedThisFrame;
        meleeAttackPressed = Mouse.current.leftButton.wasPressedThisFrame;
        rangedAttackPressed = Mouse.current.rightButton.wasPressedThisFrame;

        GroundCheck();
        stateMachine.Update();
    }

    public void EnableLight()
    {
        playerLight.SetActive(true);
    }

    public void Move()
    {
        if (dir != 0)
        {
            if (dir > 0)
                sr.flipX = false;
            else
                sr.flipX = true;
        }
        //rb.linearVelocity = new Vector2(dir * PlayerStats.instance.speed, rb.linearVelocity.y);
        float targetSpeed = dir * PlayerStats.instance.speed;

        rb.linearVelocity = new Vector2(Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, 20 * Time.fixedDeltaTime), rb.linearVelocity.y);
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void MeleeAttack()
    {
        catClaw.Attack();
    }

    public void RangedAttack()
    {
        cannedTunaW.Attack();
    }

    public void TakeDamage(float damage)
    {
        PlayerStats.instance.DecreaseNowHP(damage);
        animator.SetTrigger(damageHash);
        UIManager.instance.RefreshHPUI();
        if (PlayerStats.instance.nowHP <= 0)
        {
            PlayerStats.instance.nowHP = 0;
            Die();
        }
    }

    public void Die()
    {
        if (PlayerStats.instance.nowLife <= 0)
        {
            StageManager.instance.GameOver();
            PlayerStats.instance.nowLife = 0;
            return;
        }
        PlayerStats.instance.nowLife--;
        PlayerStats.instance.nowHP = PlayerStats.instance.maxHP;
        UIManager.instance.RefreshHPUI();

        Respawn();
    }
    
    void Respawn()
    {
        if (respawnPoint == null)
        {
            respawnPoint = GameObject.FindWithTag("Respawn").transform;
        }

        transform.position = respawnPoint.position;
        StageManager.instance.ResetEnemy();
    }

    public void SetRespawnPoint(Transform point)
    {
        respawnPoint = point;
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.3f, Vector2.down, 0.8f, groundLayer);

        isGround = hit.collider == null ? false : true;

        if (isGround)
            PlayerStats.instance.jumpCount = 0;
    }

    public void Jump()
    {
        if (PlayerStats.instance.jumpCount >= PlayerStats.instance.jumpCountMax)
            return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, PlayerStats.instance.jumpPower);

        if (isGround)
            PlayerStats.instance.jumpCount++;
        else
            PlayerStats.instance.jumpCount += 2;
    }

    public void Dash()
    {
        Debug.Log("dash");

        if (!PlayerStats.instance.isUnlockDash)
            return;

        if (!canDash || isDashing)
            return;

        canDash = false;
        isDashing = true;

        float direction = sr.flipX ? -1f : 1f;

        rb.linearVelocity = new Vector2(direction * PlayerStats.instance.dashPower, 0);

        StartCoroutine(DashCoolTime());
    }

    IEnumerator DashCoolTime()
    {
        yield return new WaitForSeconds(0.2f);

        isDashing = false;

        yield return new WaitForSeconds(PlayerStats.instance.dashCoolTime);
        
        canDash = true;
    }

    public void PlayMoveAnim(bool value)
    {
        animator.SetBool(isMove, value);
    }

    public void PlayDashAnim(bool value)
    {
        animator.SetBool(isDash, value);
    }

    public void PlayJumpAnim(bool value)
    {
        animator.SetBool(isJump, value);
    }

    public void PlayMeleeAttackAnim()
    {
        animator.SetTrigger(attackHash);
    }
}
