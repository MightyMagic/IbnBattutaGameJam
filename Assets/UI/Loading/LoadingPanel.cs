using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Image background;
    [SerializeField] private Image quote;

    public static LoadingPanel Instance { get; private set; }

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

    public void StartLoading()
    {
        int randomIndex = Random.Range(0, 2);
        Debug.Log(randomIndex);

        string path = $"Loading/loading{randomIndex}";
        background.sprite = Resources.Load<Sprite>(path);

        string quotePath = $"Loading/quote{randomIndex}";
        quote.sprite = Resources.Load<Sprite>(quotePath);

        loadingPanel.SetActive(true);

    }
}
