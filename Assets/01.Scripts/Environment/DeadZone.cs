using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();

            if (playerController != null)
                playerController.Die();
        }
    }
}
