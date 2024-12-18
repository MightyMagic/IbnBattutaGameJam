using System.Collections;
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
    public int metals = 0;
    public int grain = 0;
    public int spices = 0;
    public int silk = 0;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("MapScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
            StartCoroutine(LoadScene("DynamicLevelPrototype"));
        }
    }

    private IEnumerator LoadScene(string sceneName)
    {

        LoadingPanel.Instance.StartLoading();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName);
    }
}
