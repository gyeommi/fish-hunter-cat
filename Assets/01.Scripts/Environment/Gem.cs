using UnityEngine;

public class Gem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundManager.instance.PlaySFX(SFXType.SmallFish);

            UIManager.instance.SetGemText();
            gameObject.SetActive(false);
        }
    }
}
