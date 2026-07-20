using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ModeScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button startButton;
    private Button trainingButton;
    private Button backButton;
    public AudioClip click;
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
        PlayClick();
        Invoke("LoadStart", 0.025f);
    }
    void LoadStart()
    {
        SceneManager.LoadScene("Game");
    }

    void Training()
    {
        PlayClick();
        Invoke("LoadTraining", 0.025f);
    }

    void LoadTraining()
    {
        SceneManager.LoadScene("Game_Training");
    }

    void Back()
    {
        PlayClick();
        Invoke("LoadBack", 0.025f);
    }
    void LoadBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void PlayClick()
    {
        AudioSource.PlayClipAtPoint(click, transform.position);  
    }
}
