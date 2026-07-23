using UnityEngine;

public class GoldFish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundManager.instance.PlaySFX(SFXType.GoldFish);

            PlayerStats.instance.SaveData();
            StageManager.instance.GameEnd();
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
    }
}
