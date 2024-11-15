using UnityEngine;

public class GlobeController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float rotateX = vertical * rotationSpeed * Time.deltaTime;
        float rotateY = horizontal * rotationSpeed * Time.deltaTime;

        transform.Rotate(rotateX, rotateY, 0, Space.World);
    }
}
