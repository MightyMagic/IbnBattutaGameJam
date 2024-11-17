using UnityEngine;
using UnityEngine.UI;

public class TravelButton : MonoBehaviour
{
    [SerializeField] private Button travelButton;

    void Start()
    {
        travelButton.onClick.RemoveAllListeners();
        travelButton.onClick.AddListener(Travel);
    }

    private void Travel()
    {
        StateManager.Instance.StartTravel();
    }
}
