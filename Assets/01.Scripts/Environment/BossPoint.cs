using UnityEngine;

public class BossPoint : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().SetRespawnPoint(respawnPoint);
            StageManager.instance.DestroyEnemy();
            gameObject.SetActive(false);
        }
    }
}
