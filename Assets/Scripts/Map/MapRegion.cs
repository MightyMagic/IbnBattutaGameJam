using UnityEngine;
using UnityEngine.UI;

public class MapRegion : MonoBehaviour
{

    [Header("Region Data")]
    [SerializeField] public string regionName;
    [SerializeField] public string resourceType;
    [SerializeField] private string maxTrade;
    [SerializeField] private string midTrade;
    [SerializeField] private string minTrade;

    [Header("Region UI")]
    [SerializeField] private Image resourceImage;

    [SerializeField] private float scaleMultiplier = 1.2f;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;

        if (resourceImage != null)
        {
            resourceImage.sprite = Resources.Load<Sprite>($"Icons/{resourceType}");
        }
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
