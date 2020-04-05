using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryScript : MonoBehaviour
{
    [Header("Input Values")]
    [SerializeField] string[] orderedSlideText = new string[5];
    [SerializeField] Sprite[] orderedSceneImages = new Sprite[5];
    [Header("Canvas Elements")]
    [SerializeField] Image canvasStoryBGElement;
    [SerializeField] Text canvasTextElement;

    int currentScene = 0;

    void Start()
    {
        ChangeScene();  // Sets bg sprite to first
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentScene++;
            ChangeScene();
        }
    }

    void ChangeScene()  // Changes BG sprite depending on the current scene
    {
        if(currentScene < orderedSceneImages.Length)
        {
            canvasStoryBGElement.sprite = orderedSceneImages[currentScene];
            canvasTextElement.text = orderedSlideText[currentScene];
        }
        else
        {
            Debug.Log("End of scenes.");
            SceneManager.LoadScene("Lvl_01");   // Load the first level
        }
    }
}
