using DG.Tweening;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 dir;
    [SerializeField] private float delay;
    [SerializeField] private Ease ease;

    private Vector3 startPos;
    private Vector3 endPos;

    private bool isMoving = false;

    void Start()
    {
        startPos = transform.position;
        endPos = transform.position + dir;

        if (!isMoving)
        {
            transform.DOMove(dir, delay).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
