using UnityEngine;

public class Gem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            UIManager.instance.SetGemText();
            gameObject.SetActive(false);
        }
    }
}
