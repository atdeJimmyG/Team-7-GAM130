using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    // Public flag, so can be used whenever to check if the game is paused
    public SceneFader scenefader;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
                Pause();
        }
    }
    // Press ESC to pause/unpause

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        GameIsPaused = false;
    }
    // Closes the UI and resumes time

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        GameIsPaused = true;
    }
    // Opens the UI and stops time

    public void Quit()
    {
        scenefader.fadeTo("MainMenu");
    }
    // Loads the "MainMenu" scene
}
