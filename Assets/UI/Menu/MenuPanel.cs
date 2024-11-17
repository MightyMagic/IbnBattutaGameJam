using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void PlayGame()
    {
        menuPanel.SetActive(false);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
