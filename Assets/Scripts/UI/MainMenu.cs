using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneFader scenefader;
    public string NextLevel;

    // When called tells thge scene fader to open the next level.
    public void PlayGame()
    {
        scenefader.fadeTo(NextLevel);
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
