using UnityEngine;

public class SmallFish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StageManager.instance.NextStage();
            gameObject.SetActive(false);
        }
    }
}
