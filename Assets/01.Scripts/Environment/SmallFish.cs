using UnityEngine;

public class SmallFish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundManager.instance.PlaySFX(SFXType.SmallFish);

            PlayerStats.instance.SaveData();
            StageManager.instance.NextStage();
            gameObject.SetActive(false);
        }
    }
}
