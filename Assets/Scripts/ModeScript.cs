using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ModeScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button startButton;
    private Button trainingButton;
    private Button splitscreenButton;
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
        splitscreenButton = uiDocument.rootVisualElement.Q<Button>("Vs");
        backButton = uiDocument.rootVisualElement.Q<Button>("Back");

        startButton.clicked += StartGame;
        trainingButton.clicked += Training;
        splitscreenButton.clicked += Splitscreen;
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

    void Splitscreen()
    {
        PlayClick();
        Invoke("LoadSplitscreen", 0.025f);
    }

    void LoadSplitscreen()
    {
        SceneManager.LoadScene("Difficulty_Selection");
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
