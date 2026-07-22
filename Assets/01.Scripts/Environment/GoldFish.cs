using UnityEngine;

public class GoldFish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStats.instance.SaveData();
            StageManager.instance.GameEnd();
            gameObject.SetActive(false);
        }
    }
}
