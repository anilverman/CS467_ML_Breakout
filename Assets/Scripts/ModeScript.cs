using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ModeScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button startButton;
    private Button trainingButton;
    private Button splitscreenButton;
    private Button humansButton;
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
        humansButton = uiDocument.rootVisualElement.Q<Button>("HvH");
        backButton = uiDocument.rootVisualElement.Q<Button>("Back");

        startButton.clicked += StartGame;
        trainingButton.clicked += Training;
        splitscreenButton.clicked += Splitscreen;
        humansButton.clicked += HumanVsHuman;
        backButton.clicked += Back;

    }

    void StartGame()
    {
        PlayClick();
        Invoke("LoadStart", 0.025f);
    }
    
    void LoadStart()
    {
        // Set the last scene as the main single player game
        PlayerPrefs.SetString("LastScene", "Game");
        // Removes the set winner if the player goes from a splitscreen mode and back to a single player mode so that the eventual Game Over screen is accurate
        PlayerPrefs.DeleteKey("Winner");
        SceneManager.LoadScene("Game");
    }

    void Training()
    {
        PlayClick();
        Invoke("LoadTraining", 0.025f);
    }

    void LoadTraining()
    {
        // Set the last scene as the ML Training mode
        PlayerPrefs.SetString("LastScene", "Game_Training");
        // Removes the set winner if the player goes from a splitscreen mode and back to a single player mode so that the eventual Game Over screen is accurate
        PlayerPrefs.DeleteKey("Winner");
        SceneManager.LoadScene("Game_Training");
    }

    void Splitscreen()
    {
        PlayClick();
        Invoke("LoadSplitscreen", 0.025f);
    }

    void LoadSplitscreen()
    {
        // Set the last scene as the Human vs AI Splitscreen Mode (Will likely need to be changed to point to specific difficulty scenes when those are implemented)
        PlayerPrefs.SetString("LastScene", "Game_Splitscreen");
        SceneManager.LoadScene("Difficulty_Selection");
    }

    void HumanVsHuman()
    {
        PlayClick();
        Invoke("LoadHumanVsHuman", 0.025f);
    }

    void LoadHumanVsHuman()
    {
        PlayerPrefs.SetString("LastScene", "Game_Splitscreen_HvH");
        SceneManager.LoadScene("Game_Splitscreen_HvH");
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
