using UnityEngine;

public class LightItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundManager.instance.PlaySFX(SFXType.SmallFish);

            collision.GetComponent<PlayerController>().EnableLight();
            gameObject.SetActive(false);
        }
    }
}
