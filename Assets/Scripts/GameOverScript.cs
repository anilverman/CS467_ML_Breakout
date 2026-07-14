using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button restartButton;
    private Button mainMenuButton;
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
        SceneManager.LoadScene("Game");
    }
    void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
