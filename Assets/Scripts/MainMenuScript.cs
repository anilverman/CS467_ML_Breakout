using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button startButton;
    private Button quitButton;
    private Button leaderboardButton;
    void OnEnable()
    {
        Buttons();
    }
    void Buttons()
    {
        uiDocument = GetComponent<UIDocument>();
        startButton = uiDocument.rootVisualElement.Q<Button>("Start");
        quitButton = uiDocument.rootVisualElement.Q<Button>("Quit");
        leaderboardButton = uiDocument.rootVisualElement.Q<Button>("Leaderboard");

        startButton.clicked += StartGame;
        quitButton.clicked += QuitGame;
        leaderboardButton.clicked += Leaderboard;

    }

    void StartGame()
    {
        SceneManager.LoadScene("Mode");
    }
    void QuitGame()
    {
        Application.Quit();
    }
    void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
