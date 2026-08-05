using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

/// <summary>
/// Class to handle the scoring and lives features of the game.
/// </summary>
public class ScoreScript : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private Label scoreText;
    private int score = 0;
    private Label livesText;
    public int lives = 3;
    private float timer = 0f;
    private bool timeOn = true;
    [SerializeField] private BallScript ballScript;


    /// <summary>
    /// Initializes references to the UI labels.
    /// </summary>
    void Start()
    {
        scoreText = uiDocument.rootVisualElement.Q<Label>("Score");
        livesText = uiDocument.rootVisualElement.Q<Label>("Lives");
    }

    /// <summary>
    /// Updates the score and awards bonus lives.
    /// </summary>
    /// <param name="points"></param>
    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
        if (score > 0 && score % 9800 == 0 && lives < 4)
        {
            GainLife();
        }
    }

    /// <summary>
    /// Continuously updates the game timer.
    /// </summary>
    public void Update()
    {
       if (timeOn)
        {
            timer += Time.deltaTime;
        } 
    }
    /// <summary>
    /// Updates the UI to show how many lives are left. Called by Gain and Lose Life methods.
    /// </summary>
    public void UpdateLives()
    {
        switch (lives)
        {
        case 4:
            livesText.text = "Lives: ****";
            break;
        case 3:
            livesText.text = "Lives: ***";
            break;
        case 2:
            livesText.text = "Lives: **";
            break;
        case 1:
            livesText.text = "Lives: *";
            break;    
        }   
    }
    /// <summary>
    /// Handles losing a life when ball is lost off screen.
    /// </summary>
    public void LoseLife()
    {
        lives--;
        UpdateLives();
        if (lives == 0)
        {   
            // Stops the timer and saves the high score and time
            timeOn = false;
            livesText.text = "Lives: ";

            // Store the final score and time for the Game Over screen.
            PlayerPrefs.SetInt("CurrentScore", score);
            PlayerPrefs.SetFloat("CurrentTime", timer);
            PlayerPrefs.Save();

            ballScript.StopBall();
            gameObject.SetActive(false);

            SceneManager.LoadScene("GameOver");
        }    
        }
    /// <summary>
    /// Handles gaining lives when score threshhold is met.
    /// </summary>
    public void GainLife()
    {
        lives++;
        UpdateLives();
    }
}
