using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(settingsMenu.activeSelf)
            {
                settingsMenu.SetActive(false);
            }
        }
    }

    public void ShowSettingsMenu()
    {
        if(settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
        }
        else
        {
            settingsMenu.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("StoryScene");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
