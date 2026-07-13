using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private SpriteRenderer playerSpriteRenderer;

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpPower = 5f;

    float dir;
    bool isGround;

    int jumpCount;
    int jumpCountMax;

    [SerializeField] LayerMask groundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        jumpCount = 0;
        jumpCountMax = 2;
    }

    void Update()
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
    }

    private void FixedUpdate()
    {
        if (dir != 0)
        {
            if (dir > 0)
                playerSpriteRenderer.flipX = false;
            else
                playerSpriteRenderer.flipX = true;
        }
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.3f, Vector2.down, 0.8f, groundLayer);

        isGround = hit.collider == null ? false : true;

        if (isGround)
            jumpCount = 0;
    }

    void Jump()
    {
        if (jumpCount >= jumpCountMax)
            return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);

        if (isGround)
            jumpCount++;
        else
            jumpCount += 2;
    }
}
