using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }

    [Header("Location References")]
    [SerializeField] private string _from;
    [SerializeField] private string _to;
    [SerializeField] private string _trading;
    [SerializeField] private float _travelDistance;

    [Header("Resources")]
    [SerializeField] private int metals = 0;
    [SerializeField] private int grain = 0;
    [SerializeField] private int spices = 10;
    [SerializeField] private int silk = 0;

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

    public void SetTravel(string from, string to, float travelDistance, string trading)
    {
        _from = from;
        _to = to;
        _trading = trading;
        _travelDistance = travelDistance;
    }

    public void StartTravel()
    {
        if (_travelDistance > 0)
        {
            SceneManager.LoadScene("DynamicLevelPrototype");
        }
    }
}
