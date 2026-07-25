using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandlerScript : MonoBehaviour
{
    public ScoreScript playerScore;

    // Update is called once per frame
    void Update()
    {
        // Show the Game Over Scene if the player loses all lives
        if(playerScore.lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
