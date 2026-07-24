using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossPoint : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;
    [SerializeField] Light2D light;
    [SerializeField] GameObject bossHPCanvas;
    [SerializeField] GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundManager.instance.PlaySFX(SFXType.SmallFish);

            collision.gameObject.GetComponent<PlayerController>().SetRespawnPoint(respawnPoint);
            StageManager.instance.DestroyEnemy();

            Color color;
            if (ColorUtility.TryParseHtmlString("#5E1A1B", out color))
                light.color = color;

            bossHPCanvas.SetActive(true);
            boss.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
