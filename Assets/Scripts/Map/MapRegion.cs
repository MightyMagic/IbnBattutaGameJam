using UnityEngine;

public class MapRegion : MonoBehaviour
{
    private Vector3 originalScale;

    [SerializeField] private float scaleMultiplier = 1.2f;

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
