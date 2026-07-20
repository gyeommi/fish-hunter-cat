using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] Transform target;

    private Transform[] backs;

    [SerializeField] float backgroundWidth;
    float totalWidth;
    float widthCount;

    void Start()
    {
        backs = new Transform[transform.childCount];
        widthCount = 3f;

        for (int i = 0; i < transform.childCount; i++)
        {
            backs[i] = transform.GetChild(i);
        }
        totalWidth = backgroundWidth * backs.Length;
    }

    void Update()
    {
        float left = target.position.x - backgroundWidth * widthCount / 2;
        float right = target.position.x + backgroundWidth * widthCount / 2;

        for (int i = 0; i < backs.Length; i++)
        {
            if (backs[i].position.x < left)
            {
                backs[i].position += Vector3.right * totalWidth;
            }
            else if (backs[i].position.x > right)
            {
                backs[i].position += Vector3.left * totalWidth;
            }
        }
    }
}
