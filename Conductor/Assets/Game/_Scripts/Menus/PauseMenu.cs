using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Gerad Paris & Robert Thomas
// This script combines Gerad's SettingsMenuUI script with my Pause Menu Script
// April 2 2020

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused { get { return gamePaused; } }

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField]
    private PostProcessVolume processVolume;
    [SerializeField]
    private AudioMixer mixer;

    private Bloom bloom = null;
    private static bool gamePaused = false;

    // Gerad Paris & Robert Thomas
    void Start()
    {
        processVolume.profile.TryGetSettings(out bloom);
        pauseMenu.SetActive(gamePaused);
        settingsMenu.SetActive(gamePaused);
    }

    // Robert Thomas
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                if (settingsMenu.activeSelf)
                    HideSettingsMenu();
                else
                    ResumeGame();
            }
            else
                PauseGame();
        }
    }

    // Robert Thomas
    public void PauseGame()
    {
        gamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    // Robert Thomas
    public void ResumeGame()
    {
        gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    // Robert Thomas & Gerad Paris
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    // Robert Thomas
    public void ShowSettingsMenu()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    // Robert Thomas
    public void HideSettingsMenu()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    // Robert Thomas
    public void QuitGame()
    {
        Application.Quit();
    }

    // Gerad Paris
    public void UpdateBloom(float newBloomValue)
    {
        if (bloom != null)
        {
            bloom.intensity.value = newBloomValue;
        }
    }

    // Gerad Paris
    public void UpdateMasterVolume(float newMasterVolume)
    {
        mixer.SetFloat("VolumeMaster", Mathf.Log10(newMasterVolume) * 20);
    }

    // Gerad Paris
    public void UpdateMusicVolume(float newMusicVolume)
    {
        mixer.SetFloat("VolumeMusic", Mathf.Log10(newMusicVolume) * 20);
    }

    // Gerad Paris
    public void UpdateEffectsVolume(float newEffectsVolume)
    {
        mixer.SetFloat("VolumeEffects", Mathf.Log10(newEffectsVolume) * 20);
    }
}
