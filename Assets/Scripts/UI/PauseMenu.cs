using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    // Public flag, so can be used whenever to check if the game is paused
    private SceneFader scenefader;
    public Canvas pauseMenuUI;

    private void Start()
    {
        scenefader = GameObject.FindGameObjectWithTag("SceneFader").GetComponent<SceneFader>();
    }

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
        pauseMenuUI.enabled = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
    }
    // Closes the UI and resumes time

    public void Pause()
    {
        pauseMenuUI.enabled = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
    }
    // Opens the UI and stops time

    public void Quit()
    {
        Resume();
        scenefader.fadeTo("MainMenu");
    }
    // Loads the "MainMenu" scene
}
