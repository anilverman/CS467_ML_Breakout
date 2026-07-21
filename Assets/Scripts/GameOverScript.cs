using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button restartButton;
    private Button mainMenuButton;
    public AudioClip click;
    void OnEnable()
    {
        Buttons();
    }
    void Buttons()
    {
        uiDocument = GetComponent<UIDocument>();
        restartButton = uiDocument.rootVisualElement.Q<Button>("Restart");
        mainMenuButton = uiDocument.rootVisualElement.Q<Button>("MainMenu");

        restartButton.clicked += Restart;
        mainMenuButton.clicked += MainMenu;

    }

    void Restart()
    {
        PlayClick();
        Invoke("LoadRestart", 0.025f);
    }
    void LoadRestart()
    {
        SceneManager.LoadScene("Game");
    }
    void MainMenu()
    {
        PlayClick();
        Invoke("LoadMainMenu", 0.025f);
    }
    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void PlayClick()
    {
        AudioSource.PlayClipAtPoint(click, transform.position);  
    }
}
