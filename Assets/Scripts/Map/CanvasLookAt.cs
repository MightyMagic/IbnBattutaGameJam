using UnityEngine;

public class CanvasLookAt : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;

    void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

void LateUpdate()
    {
        if (targetCamera != null)
        {
            transform.LookAt(targetCamera.transform);

            transform.rotation = Quaternion.LookRotation(transform.position - targetCamera.transform.position);
        }
    }
}
