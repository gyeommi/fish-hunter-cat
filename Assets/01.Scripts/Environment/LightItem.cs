using UnityEngine;

public class LightItem : MonoBehaviour
{
    [SerializeField] GameObject playerLight;

    private void Awake()
    {
        if (playerLight == null)
        {
            playerLight = GameObject.FindWithTag("Player").transform.GetChild(1).gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerLight.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
