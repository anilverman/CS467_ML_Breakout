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
            SceneManager.LoadScene("GameOver");
        }
    }
}
