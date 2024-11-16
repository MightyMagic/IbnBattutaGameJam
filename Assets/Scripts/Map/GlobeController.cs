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

    [Header("Travel Trajectory")]
    [SerializeField] private int curveResolution = 10;
    [SerializeField] private float curveHeight = 1f;

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

        if (targetRegion != null && currentRegion != targetRegion)
        {
            Vector3 controlPoint = (currentRegion.transform.position + targetRegion.transform.position) / 2;
            controlPoint += Vector3.up * curveHeight;

            // Vector3 midPoint = (currentRegion.transform.position + targetRegion.transform.position) / 2;
            // Vector3 direction = (targetRegion.transform.position - currentRegion.transform.position).normalized;
            // Vector3 curveDirection = Vector3.Cross(direction, Vector3.up).normalized;
            // Vector3 controlPoint = midPoint + curveDirection * curveHeight;

            Vector3[] points = new Vector3[curveResolution];
            for (int i = 0; i < curveResolution; i++)
            {
                float t = i / (float)(curveResolution - 1);
                points[i] = CalculateBezierPoint(t, currentRegion.transform.position, controlPoint, targetRegion.transform.position);
            }

            lineRenderer.positionCount = points.Length;
            lineRenderer.SetPositions(points);
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        return (1 - t) * (1 - t) * p0 +
               2 * (1 - t) * t * p1 +
               t * t * p2;
    }

}
