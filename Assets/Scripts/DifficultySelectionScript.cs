using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DifficultySelectionScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button beginnerButton;
    private Button mediumButton;
    private Button challengingButton;
    private Button backButton;
    public AudioClip click;

    void OnEnable()
    {
        Buttons();
    }

    void Buttons()
    {
        // find document
        uiDocument = GetComponent<UIDocument>();

        // find buttons
        beginnerButton = uiDocument.rootVisualElement.Q<Button>("Beginner");
        mediumButton = uiDocument.rootVisualElement.Q<Button>("Medium");
        challengingButton = uiDocument.rootVisualElement.Q<Button>("Challenging");
        backButton = uiDocument.rootVisualElement.Q<Button>("Back");


        // register click events
        beginnerButton.clicked += Beginner;
        mediumButton.clicked += Medium;
        challengingButton.clicked += Challenging;
        backButton.clicked += Back;
    }

    void LoadGame()
    {
        SceneManager.LoadScene("Game_SplitScreen");
    }

    void LoadBack()
    {
        SceneManager.LoadScene("Mode");
    }

    // function to launch beginner agent
    void Beginner()
    {
        PlayClick();
        Invoke("LoadGame", 0.025f);
    }

    // function to launch medium agent
    void Medium()
    {
        PlayClick();
        Invoke("LoadGame", 0.025f);
    }

    // function to launch challenging agent
    void Challenging()
    {
        PlayClick();
        Invoke("LoadGame", 0.025f);
    }

    // Back()
    void Back()
    {
        PlayClick();
        Invoke("LoadBack", 0.025f);
    }

    void PlayClick()
    {
        AudioSource.PlayClipAtPoint(click, transform.position);  
    }
}