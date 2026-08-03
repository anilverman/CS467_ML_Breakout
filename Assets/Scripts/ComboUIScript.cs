using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;

public class ComboUIScript : MonoBehaviour
{

    [SerializeField] private UIDocument uiDocument;
    private Label comboText;
    public float ComboDisplayTime = 3f;
    

    void Start()
    {
        // access Combo label from UI documents
        comboText = uiDocument.rootVisualElement.Q<Label>("Combo");

        // set the combo label to invisible initially
        comboText.visible = false;

        Debug.Log(comboText.text);
    }

    /// <summary>
    /// When triggered, makes combo label visible showing number of bricks broken in quick succession.
    /// </summary>
    public void DisplayCombo(int comboCount)
    {
        comboText.text = "Combo: x" + comboCount;
        comboText.visible = true;
        SetComboColor(comboCount);
        StartCoroutine(HideCombo());
    }

    /// <summary>
    /// Toggle combo label back off to not be visible after set period of display time once combo has ended
    /// </summary>
    IEnumerator HideCombo()
    {
        Debug.Log("Displaying Combo text");
        yield return new WaitForSeconds(ComboDisplayTime);
        comboText.visible = false;

        Debug.Log("Hiding Combo text");
    }

    /// <summary>
    /// As combo multiplier increases, change color of font and increase size prominence. 
    /// </summary>
    public void SetComboColor(int comboCount)
    {
        // combo of 10+ bricks set to red and increase font to 18
        if (comboCount >= 10)
        {
            comboText.style.color = Color.red;
            comboText.style.fontSize = 18;
        }

        // combo of 9 bricks set to yellow and increase font to 14
        else if (comboCount >= 9)
        {
            comboText.style.color = Color.yellow;
            comboText.style.fontSize = 14;
        } 

        // combo of 7 bricks set to magenta and increase font to 10
        else if (comboCount >= 7)
        {
            comboText.style.color = Color.magenta;
            comboText.style.fontSize = 10;
        }

        // combo of 5 bricks set to cyan and increase font to 8
        else if (comboCount >= 5)
        {
            comboText.style.color = Color.cyan;
            comboText.style.fontSize = 8;
        }

        // combo of 3+ bricks set to white and increase font to 8
        else if (comboCount >= 3)
        {
            comboText.style.color = Color.white;
            comboText.style.fontSize = 6;
        }
    }

}