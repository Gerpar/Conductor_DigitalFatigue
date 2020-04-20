using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesertThunderbussController : MonoBehaviour
{
    // Desert Thunderbuss Controller
    // Created by Jared Roberge

    // Yes, I made a functional version of Penn & Teller's Desert Bus simulator.
    // Sometimes the correct question isn't "Why?", it's "Why not?". 
    // I started this on 3/6/2020 a sort of challenge to myself.

    //----------------------------------------------------------------------
    // Variables

    public float busAccel;
    public float busStrafeSpeed;
    public float speedLimit;

    public GameObject titleScreen;
    public GameObject youWin;
    public GameObject youLose;

    public AudioSource driving;
    private enum RadioTrack { T1, T2, T3, T4, T5};
    private int currentTrack;

    private enum eGameState { STARTUP, PLAYING, WIN, LOSE };
    private int gameState = (int)eGameState.STARTUP;
    private Rigidbody rBody;

    private Collider busCol;



    // Start is called before the first frame update
    void Start()
    {
        // On start, get the rigid body of the desert thunderbus.
        rBody = GetComponent<Rigidbody>();
        busCol = GetComponent<BoxCollider>();
        driving = GetComponent<AudioSource>();

        youLose.SetActive(false);
        youWin.SetActive(false);
        titleScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == (int)eGameState.LOSE || gameState == (int)eGameState.WIN)
        {
            StopCoroutine(Driving());

            // Slow the bus to a halt
            Vector3 localVelo = transform.InverseTransformDirection(rBody.velocity);
            localVelo.z *= 0.001f;
            localVelo.x *= 0.001f;
            rBody.velocity = transform.TransformDirection(localVelo);
        }
    }

    public IEnumerator Driving()
    {
        while (true)
        {
            // First check W key
            if (Input.GetKey("w"))
            {
                if (rBody.velocity.z < speedLimit)
                {
                    rBody.AddForce(transform.forward * busAccel, ForceMode.Impulse);
                }
            }
            // Sideways movement

            if (Input.GetKey(KeyCode.A) && rBody.velocity.z > 2)
            {
                rBody.AddForce(transform.right * -busStrafeSpeed, ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.D) && rBody.velocity.z > 2)
            {
                rBody.AddForce(transform.right * busStrafeSpeed, ForceMode.Impulse);
            }

            Vector3 localVelo = transform.InverseTransformDirection(rBody.velocity);

            // Negate sideways movement
            if (Mathf.Abs(rBody.velocity.x) > 0)
            {
                localVelo.x -= localVelo.x * 0.1f;
                rBody.velocity = transform.TransformDirection(localVelo);
            }

            // Negate forwards movement
            if (Mathf.Abs(rBody.velocity.z) > 0)
            {
                localVelo.z -= localVelo.z * 0.02f;
                rBody.velocity = transform.TransformDirection(localVelo);
            }
            yield return new WaitForSeconds(0.1f);
        }

    }


    public void OnTriggerEnter(Collider other)
    {
        // If you drive off of the road
        if (other.CompareTag("Deathplane"))
        {
            StopCoroutine(Driving());
            gameState = (int)eGameState.LOSE;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        // if you win
        if (other.CompareTag("Finish"))
        {
            gameState = (int)eGameState.WIN;
            StopCoroutine(Driving());
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnGUI()
    {
        if (gameState == (int)eGameState.STARTUP)
        {
            titleScreen.SetActive(true);

            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 60, 180, 40), "Start"))
            {
                gameState = (int)eGameState.PLAYING;
                titleScreen.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
                StartCoroutine(Driving());
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 110, 180, 40), "Quit"))
            {
                SceneManager.LoadScene(0);
            }
        }

        if (gameState == (int)eGameState.LOSE)
        {
            youLose.SetActive(true);
            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 110, 180, 40), "Main Menu"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (gameState == (int)eGameState.WIN)
        {
            youWin.SetActive(true);
            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 110, 180, 40), "Main Menu"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
