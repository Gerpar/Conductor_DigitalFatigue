using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    // Author: Jared J. Roberge
    // 2/26/2020
    // This is a GUI debug menu, because I felt like doing 
    // something fun. I'll add more to this as time goes on.

    // A fun little cheat/debug menu. Press ~ to access it.
    // Why not hide some weirdness in our game.

    public bool CheatsEnabled = false;

    // Jobel Mode
    [SerializeField] AudioClip ShootJobel;
    [SerializeField] AudioClip ChargeJobel;
    [SerializeField] AudioClip ImpactJobel; // I'll use this slot later.
    [SerializeField] AudioClip JumpJobel;
    [SerializeField] AudioClip DeathJobel;

    private int timescale = 1;
    private float shotdelay = 0.8f;
    private float scaleincrement = 0.5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            CheatsEnabled = !CheatsEnabled;
        }
    }

    // If cheats are on, then show a menu
    void OnGUI()
    {
        if (CheatsEnabled)
        {
            GUI.backgroundColor = Color.green;
            GUI.Box(new Rect(20, 80, 240, 360), "Debug Menu");
            GUI.color = Color.white;

            //----------------------------------------------------------
            // Cheat 01 - Reload the Level
            if (GUI.Button(new Rect(40, 120, 200, 30), "Reload Level"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            //----------------------------------------------------------
            // Cheat 02 - Jobel Mode (Replaces Sounds with Vinesauce Joel)
            if (GUI.Button(new Rect(40, 160, 200, 30), "Uncle Jobel Mode"))
            {
                gameObject.GetComponent<ThunderbussController>().fireSound = ShootJobel;
                gameObject.GetComponent<ThunderbussController>().chargeSound = ChargeJobel;
                gameObject.GetComponent<Health>().hurtSound = DeathJobel;
                gameObject.GetComponent<PlayerMove>().jumpSound = JumpJobel;
            }

            //----------------------------------------------------------
            // Cheat 03 - Change Fire Rate
            GUI.Box(new Rect(80, 200, 120, 30), "Fire Delay = " + shotdelay);

            if (GUI.Button(new Rect(210, 200, 30, 30), "+"))
            {
                if (shotdelay <= 5.0f)
                {
                    shotdelay += 0.1f;
                    gameObject.GetComponent<ThunderbussController>().chargeTime = shotdelay;
                }
            }
            if (GUI.Button(new Rect(40, 200, 30, 30), "-"))
            {
                if (shotdelay >= 0.0f)
                {
                    shotdelay -= 0.1f;
                    gameObject.GetComponent<ThunderbussController>().chargeTime = shotdelay;
                }
                else if (shotdelay < 0.0f)
                {
                    shotdelay = 0.0f;
                }
            }


            //----------------------------------------------------------
            // Cheat 04 - Change Timescale
            GUI.Box(new Rect(80, 240, 120, 30), "Timescale =" + Time.timeScale);

            if (GUI.Button(new Rect(210, 240, 30, 30), "+"))
            {
                if (Time.timeScale <= 5)
                {
                    Time.timeScale += 0.1f;
                }
            }
            if (GUI.Button(new Rect(40, 240, 30, 30), "-"))
            {
                if (Time.timeScale >= 0)
                {
                    Time.timeScale -= 0.1f;
                }
            }

            //----------------------------------------------------------
            // Cheat 05 - Change player scale
            GUI.Box(new Rect(80, 280, 120, 30), "Player Size = " + gameObject.GetComponent<Transform>().localScale.x);

            if (GUI.Button(new Rect(210, 280, 30, 30), "+"))
            {
                if (gameObject.GetComponent<Transform>().localScale.x < 5.0f && gameObject.GetComponent<Transform>().localScale.y < 5.0f && gameObject.GetComponent<Transform>().localScale.z < 5.0f)
                {
                    Vector3 newScale = gameObject.GetComponent<Transform>().localScale;
                    newScale.x += scaleincrement;
                    newScale.y += scaleincrement;
                    newScale.z += scaleincrement;

                    gameObject.GetComponent<Transform>().localScale = newScale;
                }

            }
            if (GUI.Button(new Rect(40, 280, 30, 30), "-"))
            {
                if (gameObject.GetComponent<Transform>().localScale.x > 0.0f && gameObject.GetComponent<Transform>().localScale.y > 0.0f && gameObject.GetComponent<Transform>().localScale.z > 0.0f)
                {
                    Vector3 newScale = gameObject.GetComponent<Transform>().localScale;
                    newScale.x -= scaleincrement;
                    newScale.y -= scaleincrement;
                    newScale.z -= scaleincrement;

                    gameObject.GetComponent<Transform>().localScale = newScale;
                }
            }

            //----------------------------------------------------------
            // Exit menu
            if (GUI.Button(new Rect(40, 320, 200, 30), "Close Debug Menu"))
            {
                CheatsEnabled = false;
            }
        }
    }
}
