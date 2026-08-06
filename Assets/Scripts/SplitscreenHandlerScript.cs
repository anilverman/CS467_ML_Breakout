using UnityEngine;
using UnityEngine.SceneManagement;

public class SplitscreenHandlerScript : MonoBehaviour
{
    public ScoreScript player1Score;
    public ScoreScript player2Score;

    void Update()
    {
        // Show the Game Over Scene if both players have 0 lives
        if(player1Score.lives <= 0 && player2Score.lives <= 0)
        {
            // Player 1 Wins
            if (player1Score.finalScore > player2Score.finalScore)
            {
                PlayerPrefs.SetInt("Winner", 1);
                // Set Player 1's score and time as the one to be saved
                PlayerPrefs.SetInt("CurrentScore", player1Score.finalScore);
            }
            // Player 2 Wins
            else if (player2Score.finalScore > player1Score.finalScore)
            {
                PlayerPrefs.SetInt("Winner", 2);
                PlayerPrefs.SetInt("CurrentScore", player2Score.finalScore);
            }
            // Draw
            else
            {
                PlayerPrefs.SetInt("Winner", 0);
                // If it is draw both scores are the same so we can just save Player 1's
                PlayerPrefs.SetInt("CurrentScore", player1Score.finalScore);
            }

            SceneManager.LoadScene("GameOver");
        }
    }
}
