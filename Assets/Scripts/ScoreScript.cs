using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public UIDocument uiDocument;
    private Label scoreText;
    private float score = 0f;

    void Start()
    {
        scoreText = uiDocument.rootVisualElement.Q<Label>("Score");
    }

    public void UpdateScore(int points)
    {
    score += points;
    scoreText.text = "Score: " + score;
    }
}
