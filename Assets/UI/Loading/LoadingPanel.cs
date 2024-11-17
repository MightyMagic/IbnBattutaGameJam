using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    public List<Image> loadingImages;
    public List<Image> loadingTexts;

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
        loadingPanel.SetActive(true);
    }
}
