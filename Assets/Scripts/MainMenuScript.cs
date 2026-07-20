using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button startButton;
    private Button quitButton;
    private Button leaderboardButton;
    public AudioClip click;
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
        PlayClick();
        Invoke("LoadStart", 0.025f);
    }
    void LoadStart()
    {
        SceneManager.LoadScene("Mode");
    }
    void QuitGame()
    {
        PlayClick();
        Invoke("LoadQuit", 0.025f);
    }
    void LoadQuit()
    {
        Application.Quit();
    }
    void Leaderboard()
    {
        PlayClick();  
        Invoke("LoadLeaderboard", 0.05f);
    }
    void LoadLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
    void PlayClick()
    {
        AudioSource.PlayClipAtPoint(click, transform.position);  
    }
}
