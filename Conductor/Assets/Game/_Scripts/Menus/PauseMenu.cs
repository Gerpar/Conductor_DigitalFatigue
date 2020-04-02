using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas
// March 12 2020

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused { get { return gamePaused; } }

    [SerializeField]
    private GameObject pauseMenu;
    private static bool gamePaused = false;

    void Start()
    {
        pauseMenu.SetActive(gamePaused);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        gamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Menu");
    }

    public void ShowOptionsMenu()
    {
        Debug.Log("Options");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
