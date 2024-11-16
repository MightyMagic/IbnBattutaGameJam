using UnityEngine;

public class MapRegion : MonoBehaviour
{

    [Header("Region Data")]
    [SerializeField] private Region region;

    [SerializeField] private float scaleMultiplier = 1.2f;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnMouseOver()
    {
        transform.localScale = originalScale * scaleMultiplier;
    }

    private void OnMouseExit()
    {
        transform.localScale = originalScale;
    }
}
