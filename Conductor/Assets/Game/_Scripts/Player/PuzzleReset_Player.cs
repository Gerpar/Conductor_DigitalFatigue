using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleReset_Player : MonoBehaviour
{
    [SerializeField] float resetTime; // How long the button needs to be held
    [SerializeField] Image resetCrosshair;

    float resetTimer = 0;

    // Start is called before the first frame update

    // Update is called once per frame
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
