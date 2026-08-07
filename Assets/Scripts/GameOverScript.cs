using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button restartButton;
    private Button mainMenuButton;
    private TextField initialsField;
    private Label gameOver;
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
        initialsField = uiDocument.rootVisualElement.Q<TextField>("Initials");
        gameOver = uiDocument.rootVisualElement.Q<Label>("GameOver");

        // Change the text on the Game Over Screen based on who won the game
        int winner = PlayerPrefs.GetInt("Winner", -1);
        switch (winner)
        {
            case 1:
                gameOver.text = "Player 1 Wins!";
                break;
            case 2:
                gameOver.text = "Player 2 Wins!";
                break;
            case 0:
                gameOver.text = "Draw!";
                break;
            default:
                gameOver.text = "Game Over!";
                break;
        }

        restartButton.SetEnabled(false);
        mainMenuButton.SetEnabled(false);

        initialsField.RegisterValueChangedCallback(OnInitialsChanged);

        restartButton.clicked += Restart;
        mainMenuButton.clicked += MainMenu;

    }

    /// <summary>
    /// Changes the initials to uppercase and enables the buttons after 3 are entered.
    /// </summary>
    /// <param name="evt"></param>
    void OnInitialsChanged(ChangeEvent<string> evt)
    {
        // Force uppercase.
        string initials = evt.newValue.ToUpper();

        initialsField.SetValueWithoutNotify(initials);

        bool valid = initials.Length == 3;

        restartButton.SetEnabled(valid);
        mainMenuButton.SetEnabled(valid);
    }

    /// <summary>
    /// Saves the players initials, score and time to the leaderboard
    /// </summary>
    void SaveLeaderboardEntry()
    {
        int score = PlayerPrefs.GetInt("CurrentScore");
        float time = PlayerPrefs.GetFloat("CurrentTime");
        string initials = initialsField.value;

        LeaderboardScript.SaveScore(score, time, initials);
    }

    void Restart()
    {
        SaveLeaderboardEntry();
        PlayClick();
        Invoke("LoadRestart", 0.025f);
    }
    void LoadRestart()
    {
        string lastGameMode = PlayerPrefs.GetString("LastScene");
        SceneManager.LoadScene(lastGameMode);
    }
    void MainMenu()
    {
        SaveLeaderboardEntry();
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
