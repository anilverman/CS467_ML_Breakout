using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ModeScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button startButton;
    private Button backButton;
    void OnEnable()
    {
        Buttons();
    }
    void Buttons()
    {
        uiDocument = GetComponent<UIDocument>();
        startButton = uiDocument.rootVisualElement.Q<Button>("1Player");
        backButton = uiDocument.rootVisualElement.Q<Button>("Back");

        startButton.clicked += StartGame;
        backButton.clicked += Back;

    }

    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
