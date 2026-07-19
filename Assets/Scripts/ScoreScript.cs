using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Label scoreText;
    private int score = 0;
    private Label livesText;
    private int lives = 3;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("Score");
        livesText = uiDocument.rootVisualElement.Q<Label>("Lives");
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void LoseLife()
    {
        lives--;
        switch (lives)
        {
            case 3:
                livesText.text = "Lives: ***";
                break;
            case 2:
                livesText.text = "Lives: **";
                break;
            case 1:
                livesText.text = "Lives: *";
                break;
            case 0:
                LeaderboardScript.SaveScore(score);
                SceneManager.LoadScene("GameOver");
                break;
        }
    }
}
