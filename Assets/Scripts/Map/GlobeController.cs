using Unity.VisualScripting;
using UnityEngine;

public class GlobeController : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 100f;

    [Header("Regions")]
    [SerializeField] private GameObject currentRegion;
    [SerializeField] private GameObject targetRegion;
    [SerializeField] private float travelDistance;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip pickRegionSound;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
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
                {
                    targetRegion = hit.transform.gameObject;

                    travelDistance = Vector3.Distance(
                        currentRegion.transform.position,
                        targetRegion.transform.position
                    ) * 1000f;

                    StateManager.Instance.SetTravel(
                        currentRegion.GetComponent<MapRegion>().regionName,
                        targetRegion.GetComponent<MapRegion>().regionName,
                        travelDistance,
                        targetRegion.GetComponent<MapRegion>().resourceType
                    );

                    AudioSource.PlayClipAtPoint(pickRegionSound, Camera.main.transform.position);
                }
                else {
                    targetRegion = null;
                    lineRenderer.SetPositions(new Vector3[0]);
                }
            }
        }
    }
}
