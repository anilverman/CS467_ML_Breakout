using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LeaderboardUIScript : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button backButton;
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
        SceneManager.LoadScene("MainMenu");
    }

}
