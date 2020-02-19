using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public SceneFader scenefader;
    public string NextLevel;
    public Text text;
    public GameObject Menu;

    private int fontsize = 75;
    private int index = 1;

    [SerializeField] private Button levelSelectButton;
    [SerializeField] private Text levelSelectText;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        index = PlayerPrefs.GetInt("levelReached", 1);
        if (index + 1 > 3)
        {
            text.text = "CONTINUE";
            text.fontSize = fontsize;
        }
        else
        {
            levelSelectButton.interactable = false;
            levelSelectText.color = new Color(255f, 255f, 255f, .5f);
        }

        index = PlayerPrefs.GetInt("levelReached", 1);
        if (index + 1 >= 2)
        {
            PlayerPrefs.SetInt("levelReached", 2);
        }
        else if (index == 0)
        {
            PlayerPrefs.SetInt("levelReached", 2);
        }
    }

    // When called tells thge scene fader to open the next level.
    public void PlayGame()
    {
        scenefader.fadetoindex(index);
    }

    // When called quits the application
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("ExitGame!");
    }

    public void OpenLevelSelect()
    {
        scenefader.fadeTo("LevelSelect");
    }
}
