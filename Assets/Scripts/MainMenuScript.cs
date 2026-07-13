using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button startButton;
    private Button quitButton;
    void OnEnable()
    {
        Buttons();
    }
    void Buttons()
    {
        uiDocument = GetComponent<UIDocument>();
        startButton = uiDocument.rootVisualElement.Q<Button>("Start");
        quitButton = uiDocument.rootVisualElement.Q<Button>("Quit");

        startButton.clicked += StartGame;
        quitButton.clicked += QuitGame;

    }

    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    void QuitGame()
    {
        Application.Quit();
    }
}
