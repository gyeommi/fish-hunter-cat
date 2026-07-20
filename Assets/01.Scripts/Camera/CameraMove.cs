using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float zValue;

    private Vector3 velocity;
    private Vector3 targetPos;

    void Start()
    {
        velocity = Vector3.zero;
    }

    private void LateUpdate()
    {
        targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        float x = Mathf.Clamp(targetPos.x, -2f, 142f);
        float y = Mathf.Clamp(targetPos.y, 0f, zValue);

        targetPos = new Vector3(x, y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.2f);
    }
}
