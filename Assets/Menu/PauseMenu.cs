using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseButton;

    public GameObject pauseMenuUI;

    public GameObject OptionsMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        { 
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }

    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        PauseButton.SetActive(true);
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        PauseButton.SetActive(false);
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void LoadOptionsMenu()
    {
        pauseMenuUI.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void BackToPause()
    {
        OptionsMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
        
    }
}
