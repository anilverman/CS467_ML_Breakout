using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LeaderboardUIScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button backButton;
    public AudioClip click;

    /// <summary>
    /// Initializes the leaderboard and propagates the initials scores and times into it.
    /// </summary>
    void Start()
    {
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement root = ui.rootVisualElement;

        for (int i = 0; i < 5; i++)
        {
            Label label = root.Q<Label>("Score" + (i + 1));
            int score = PlayerPrefs.GetInt("HighScore" + i, 0);
            float time = PlayerPrefs.GetFloat("HighScoreTime" + i, 0);
            string initials = PlayerPrefs.GetString("HighScoreInitials" + i, "---");
            label.text = $"{i + 1}. {initials}:   {score} - {time:F2}s";
        }
    }
    void OnEnable()
    {
        Buttons();
    }
    void Buttons()
    {
        uiDocument = GetComponent<UIDocument>();
        backButton = uiDocument.rootVisualElement.Q<Button>("Back");
        backButton.clicked += Back;
    }

    void Back()
    {
        PlayClick();
        Invoke("LoadBack", 0.025f);
    }
    void LoadBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void PlayClick()
    {
        AudioSource.PlayClipAtPoint(click, transform.position);  
    }

}
