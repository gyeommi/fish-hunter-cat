using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float damageInterval = 1f;
    private float damageTimer = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
                playerController.TakeDamage(0.1f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            damageTimer += Time.fixedDeltaTime;

            if (damageTimer >= damageInterval)
            {
                if (playerController != null)
                    playerController.TakeDamage(0.1f);
                damageTimer = 0f;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            damageTimer = 0f;
        }
    }
}
