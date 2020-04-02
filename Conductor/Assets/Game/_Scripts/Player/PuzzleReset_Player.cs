using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleReset_Player : MonoBehaviour
{
    [SerializeField] float resetTime; // How long the button needs to be held
    [SerializeField] Image resetCrosshair;
    [Header("Puzzle reloading")]
    [SerializeField] GameObject[] orderedPuzzleObjectPrefabs;
    [SerializeField] GameObject[] orderedPuzzleObjectsInScene;

    float resetTimer = 0;
    int currentPuzzleID = 0;

    void Update()
    {
        if(Input.GetButton("Reset"))    // Reset button held
        {
            resetTimer += Time.deltaTime;
        }

        if(Input.GetButtonUp("Reset"))  // Reset button let go
        {
            resetTimer = 0;
        }


        resetCrosshair.fillAmount = resetTimer / resetTime;
        if (resetTimer >= resetTime)
        {
            ReloadCurrentPuzzle();
            resetTimer = 0;
        }
    }

    public void SetPuzzleID(int newPuzzleID)
    {
        currentPuzzleID = newPuzzleID;
    }

    public void ReloadCurrentPuzzle()
    {
        Destroy(orderedPuzzleObjectsInScene[currentPuzzleID]);  // Destroy the old puzzle
        orderedPuzzleObjectsInScene[currentPuzzleID] = Instantiate(orderedPuzzleObjectPrefabs[currentPuzzleID], null, true);   // Instantiate new object into the old position
        GetComponent<Health>().currentHealth = 0;   // Kill player
    }
}
