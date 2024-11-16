using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{

    [Header("Regions")]
    [SerializeField] private GameObject currentRegion;
    [SerializeField] private GameObject targetRegion;
    [SerializeField] private float travelDistance;

    [Header("Travel Trajectory")]
    [SerializeField] private int curveResolution = 10;
    [SerializeField] private float curveHeight = 1f;
    private LineRenderer lineRenderer;
    public static StateManager Instance { get; private set; }

    [Header("Location References")]
    [SerializeField] private string from;
    [SerializeField] private string to;
    [SerializeField] private string trading;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("MapRegion"))
                {
                    targetRegion = hit.transform.gameObject;

                    from = currentRegion.GetComponent<MapRegion>().regionName;
                    to = targetRegion.GetComponent<MapRegion>().regionName;
                    trading = targetRegion.GetComponent<MapRegion>().resourceType;

                    travelDistance = Vector3.Distance(
                        currentRegion.transform.position,
                        targetRegion.transform.position
                    ) * 1000f;
                }
                else {
                    targetRegion = null;
                }
            }
        }

        if (targetRegion != null && currentRegion != targetRegion)
        {
            Vector3 controlPoint = (currentRegion.transform.position + targetRegion.transform.position) / 2;
            controlPoint += Vector3.up * curveHeight;

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

    public void StartTravel()
    {
        if (travelDistance > 0)
        {
            SceneManager.LoadScene("DynamicLevelPrototype");
        }
    }
}
