using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ModeScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button startButton;
    private Button trainingButton;
    private Button backButton;
    void OnEnable()
    {
        Buttons();
    }
    void Buttons()
    {
        uiDocument = GetComponent<UIDocument>();
        startButton = uiDocument.rootVisualElement.Q<Button>("1Player");
        trainingButton = uiDocument.rootVisualElement.Q<Button>("Training");
        backButton = uiDocument.rootVisualElement.Q<Button>("Back");

        startButton.clicked += StartGame;
        trainingButton.clicked += Training;
        backButton.clicked += Back;

    }

    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    void Training()
    {
        SceneManager.LoadScene("Game_Training");
    }

    void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
