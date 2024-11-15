using Unity.VisualScripting;
using UnityEngine;

public class GlobeController : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 100f;

    [Header("Regions")]
    [SerializeField] private GameObject targetRegion;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float rotateX = vertical * rotationSpeed * Time.deltaTime;
        float rotateY = horizontal * rotationSpeed * Time.deltaTime;

        transform.Rotate(rotateX, rotateY, 0, Space.World);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("MapRegion"))
                    targetRegion = hit.transform.gameObject;
                else
                    targetRegion = null;
            }
        }
    }
}
