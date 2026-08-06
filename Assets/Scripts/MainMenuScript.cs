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

    // enable every active GameObject
    void OnEnable()
    {
        Buttons();
    }

    void Buttons()
    {
        // attach UIDocument
        uiDocument = GetComponent<UIDocument>();

        // find button named ("X")
        startButton = uiDocument.rootVisualElement.Q<Button>("Start");
        quitButton = uiDocument.rootVisualElement.Q<Button>("Quit");
        leaderboardButton = uiDocument.rootVisualElement.Q<Button>("Leaderboard");

        // when X button is clicked, run X function. 
        startButton.clicked += StartGame;
        quitButton.clicked += QuitGame;
        leaderboardButton.clicked += Leaderboard;

    }

    void StartGame()
    {
        PlayClick();
        Invoke("LoadStart", 0.025f); // run LoadStart after brief delay
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
