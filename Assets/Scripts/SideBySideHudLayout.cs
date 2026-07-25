using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Positions an existing ScoreUI instance over one half of the shared screen.
/// Scoring and lives remain entirely owned by the existing ScoreScript.
/// </summary>
public class SideBySideHudLayout : MonoBehaviour
{
    public bool rightSide;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Label score = root.Q<Label>("Score");
        Label lives = root.Q<Label>("Lives");

        score.style.left = Length.Percent(rightSide ? 52f : 2f);
        score.style.top = 54f;
        lives.style.left = StyleKeyword.Auto;
        lives.style.right = Length.Percent(rightSide ? 2f : 52f);
        lives.style.top = 54f;
    }
}
