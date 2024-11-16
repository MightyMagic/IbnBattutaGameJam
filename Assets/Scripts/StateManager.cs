using UnityEngine;

public class StateManager : MonoBehaviour
{

    [Header("Regions")]
    [SerializeField] private GameObject currentRegion;
    [SerializeField] private GameObject targetRegion;
    [SerializeField] private float travelDistance;

    public static StateManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
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
                    Region region = targetRegion.GetComponent<MapRegion>().GetRegion();
                    Debug.Log("Traveling to: " + region.regionName);
                    Debug.Log("Trades: " + region.resourceType);

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
    }
}
