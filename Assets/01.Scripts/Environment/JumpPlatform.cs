using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField] private float jumpPower = 15f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundManager.instance.PlaySFX(SFXType.Jump);

            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.9f)
                {
                    Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

                    if (rb != null)
                    {
                        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                    }
                    break;
                }
            }
        }

    }
}