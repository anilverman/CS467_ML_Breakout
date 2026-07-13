using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Label scoreText;
    private float score = 0f;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("Score");
    }

    public void UpdateScore(int points)
    {
    score += points;
    scoreText.text = "Score: " + score;
    }
}
