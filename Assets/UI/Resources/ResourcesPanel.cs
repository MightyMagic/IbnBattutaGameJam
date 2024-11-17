using UnityEngine;
using TMPro;

public class ResourcesPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI grainText;
    [SerializeField] private TextMeshProUGUI metalsText;
    [SerializeField] private TextMeshProUGUI silkText;
    [SerializeField] private TextMeshProUGUI spicesText;

    private void Start()
    {
        grainText.text = StateManager.Instance.grain.ToString();
        metalsText.text = StateManager.Instance.metals.ToString();
        silkText.text = StateManager.Instance.silk.ToString();
        spicesText.text = StateManager.Instance.spices.ToString();
    }
}
