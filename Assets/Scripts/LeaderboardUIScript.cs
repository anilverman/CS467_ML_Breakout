using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LeaderboardUIScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button backButton;
    public AudioClip click;
    void Start()
    {
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement root = ui.rootVisualElement;

        for (int i = 0; i < 5; i++)
        {
            Label label = root.Q<Label>("Score" + (i + 1));
            label.text = $"{i + 1}. {PlayerPrefs.GetInt("HighScore" + i, 0)}";
        }
    }
    void OnEnable()
    {
        Buttons();
    }
    void Buttons()
    {
        uiDocument = GetComponent<UIDocument>();
        backButton = uiDocument.rootVisualElement.Q<Button>("Back");
        backButton.clicked += Back;
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
