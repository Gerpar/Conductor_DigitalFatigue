using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField]
    private GameObject crosshairObject;
    [SerializeField] Slider masterVolSlider;
    [SerializeField] Slider musicVolSlider;
    [SerializeField] Slider effectsVolSlider;
    [SerializeField] Slider bloomVisSlider;

    private Bloom bloom = null;
    private static bool gamePaused = false;

    // Gerad Paris & Robert Thomas
    void Start()
    {
        Time.timeScale = 1.0f;
        processVolume.profile.TryGetSettings(out bloom);
        pauseMenu.SetActive(gamePaused);
        settingsMenu.SetActive(gamePaused);

        // Load data and set slider positions
        SetSliderPositions();
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

    // Robert Thomas & Gerad Paris
    public void PauseGame()
    {
        // Robert
        gamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        // Gerad
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crosshairObject.SetActive(false);
    }

    // Robert Thomas & Gerad Paris
    public void ResumeGame()
    {
        // Robert
        gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        // Gerad
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        crosshairObject.SetActive(true);
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
            PlayerPrefs.SetFloat("BloomSliderVal", newBloomValue);
        }
    }

    // Gerad Paris
    public void UpdateMasterVolume(float newMasterVolume)
    {
        mixer.SetFloat("VolumeMaster", Mathf.Log10(newMasterVolume) * 20);
        PlayerPrefs.SetFloat("VolumeMasterSliderVal", newMasterVolume);
    }

    // Gerad Paris
    public void UpdateMusicVolume(float newMusicVolume)
    {
        mixer.SetFloat("VolumeMusic", Mathf.Log10(newMusicVolume) * 20);
        PlayerPrefs.SetFloat("VolumeMusicSliderVal", newMusicVolume);
    }

    // Gerad Paris
    public void UpdateEffectsVolume(float newEffectsVolume)
    {
        mixer.SetFloat("VolumeEffects", Mathf.Log10(newEffectsVolume) * 20);
        PlayerPrefs.SetFloat("VolumeEffectsSliderVal", newEffectsVolume);
    }

    public void SetSliderPositions()
    {
        if (PlayerPrefs.HasKey("BloomSliderVal"))
        {
            float bloomVal = PlayerPrefs.GetFloat("BloomSliderVal");
            bloom.intensity.value = bloomVal;
            bloomVisSlider.value = bloomVal;
        }
        if (PlayerPrefs.HasKey("VolumeMasterSliderVal"))
        {
            float masVolVal = PlayerPrefs.GetFloat("VolumeMasterSliderVal");
            mixer.SetFloat("VolumeMaster", Mathf.Log10(masVolVal) * 20);
            masterVolSlider.value = masVolVal;
        }
        if (PlayerPrefs.HasKey("VolumeMusicSliderVal"))
        {
            float musVolVal = PlayerPrefs.GetFloat("VolumeMusicSliderVal");
            mixer.SetFloat("VolumeMusic", Mathf.Log10(musVolVal) * 20);
            musicVolSlider.value = musVolVal;
        }
        if (PlayerPrefs.HasKey("VolumeEffectsSliderVal"))
        {
            float effVolVal = PlayerPrefs.GetFloat("VolumeEffectsSliderVal");
            mixer.SetFloat("VolumeEffects", Mathf.Log10(effVolVal) * 20);
            effectsVolSlider.value = effVolVal;
        }
    }
}
