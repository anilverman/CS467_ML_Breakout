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
    private float timer = 0f;
    private bool timeOn = true;

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

    public void Update()
    {
       if (timeOn)
        {
            timer += Time.deltaTime;
        } 
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
                timeOn = false;
                LeaderboardScript.SaveScore(score, timer);
                SceneManager.LoadScene("GameOver");
                break;
        }
    }
}
