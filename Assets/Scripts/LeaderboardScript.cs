using UnityEngine;

public class LeaderboardScript : MonoBehaviour
{
    // Number of scores displayed
    const int numScores = 5;

    public static void SaveScore(int newScore, float newTime)
    {   
        // Create an array to hold the scores
        int[] scores = new int[numScores];
        float[] times = new float[numScores];

        // Load existing high scores
        for (int i = 0; i < numScores; i++)
        {
            scores[i] = PlayerPrefs.GetInt("HighScore" + i, 0);
            times[i] = PlayerPrefs.GetFloat("HighScoreTime" + i, 0);
        }
        
        // Checks if new score is high score and adds it if so
        for (int i = 0; i < numScores; i++)
        {
            if (newScore > scores[i] || newScore == scores[i] && newTime < times[i])
            {
                (newScore, scores[i]) = (scores[i], newScore);
                (newTime, times[i]) = (times[i], newTime);
            }
        }

        // Save new scores into the scores array
        for (int i = 0; i < numScores; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, scores[i]);
            PlayerPrefs.SetFloat("HighScoreTime" + i, times[i]);
        }
        PlayerPrefs.Save();

    }
}
