using UnityEngine;

public class LeaderboardScript : MonoBehaviour
{
    // Number of scores displayed
    const int numScores = 5;
    /// <summary>
    /// Saves the users initials, score and time into arrays for the leaderboard
    /// </summary>
    /// <param name="newScore"></param>
    /// <param name="newTime"></param>
    /// <param name="newInitials"></param>
    public static void SaveScore(int newScore, float newTime, string newInitials)
    {   
        // Create an array to hold the scores
        int[] scores = new int[numScores];
        float[] times = new float[numScores];
        string[] initials = new string[numScores];

        // Load existing high scores
        for (int i = 0; i < numScores; i++)
        {
            scores[i] = PlayerPrefs.GetInt("HighScore" + i, 0);
            times[i] = PlayerPrefs.GetFloat("HighScoreTime" + i, 0);
            initials[i] = PlayerPrefs.GetString("HighScoreInitials" + i, "---");
        }
        
        // Checks if new score is high score and adds it if so
        for (int i = 0; i < numScores; i++)
        {
            if (newScore > scores[i] || newScore == scores[i] && newTime < times[i])
            {
                // switches the new score with the old score and places it in the next position down the leaderboard
                (newScore, scores[i]) = (scores[i], newScore);
                (newTime, times[i]) = (times[i], newTime);
                (newInitials, initials[i]) = (initials[i], newInitials);
            }
        }

        // Save new scores into the arrays
        for (int i = 0; i < numScores; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, scores[i]);
            PlayerPrefs.SetFloat("HighScoreTime" + i, times[i]);
            PlayerPrefs.SetString("HighScoreInitials" + i, initials[i]);
        }
        PlayerPrefs.Save();

    }
}
