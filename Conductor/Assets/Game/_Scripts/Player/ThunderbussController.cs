using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script created by Gerad paris

/// <summary>
/// Controls the aiming of the thunderbuss, and the firing mechanisms behind it.
/// </summary>

public class ThunderbussController : MonoBehaviour
{
    // Note from Jared R: A few values have been made public. Trust me dude. Totally didn't add a stupid debug menu that changes stuff.
    [Header("General Properties")] 
    [SerializeField] Transform thunderbussHolderTransform;
    [SerializeField] Camera camera;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject crosshair;
    [SerializeField] Image crosshairCharging;
    [SerializeField] GameObject chargeTrail;

    [Header("Projectile properties")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] public float chargeTime;   // Seconds it takes for thunderbuss to charge a shot

    [Header("Audio")]
    [SerializeField] public AudioClip fireSound;
    [SerializeField] public AudioClip chargeSound;

    private Vector3 mousePosInWorld;    // Keeps track of the mouse position in the world
    private float timeToFire;           // Keeps track of what time the thunderbuss can fire again
    private AudioSource src;
    private float currentCharge = 0;
    private bool chargeSoundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        src = firePoint.GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        chargeTrail.GetComponent<TrailRenderer>().emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosInWorld();

        thunderbussHolderTransform.LookAt(mousePosInWorld); // Aim thunderbuss at mouse position

        if (Input.GetButton("Fire"))
        {
            Charging();
        }
        else if(Input.GetButtonUp("Fire"))
        {
            Firing();
        }

    }

    void GetMousePosInWorld()
    {
        crosshair.transform.position = Input.mousePosition; // Set crosshair GUI to current mouse position

        mousePosInWorld = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -camera.transform.position.z));   // Find the mouse position in the world

        mousePosInWorld.z = thunderbussHolderTransform.position.z;  // Set the Z position of the mouse position to be the same Z position as the thunderbuss
    }

    void Charging()
    {
        currentCharge += Time.deltaTime; // Add deltatime to time charged

        crosshairCharging.fillAmount = currentCharge / chargeTime;  // Update UI

        if(currentCharge >= chargeTime && !chargeSoundPlayed)   // If weapon is fully charged
        {
            src.PlayOneShot(chargeSound);   // Play sound
            chargeSoundPlayed = true;       // Stop sound from playing multiple times
            chargeTrail.GetComponent<TrailRenderer>().emitting = true;    // Set the trail to be active
        }
    }

    void Firing()
    {
        if(currentCharge >= chargeTime)
        {
            GameObject newOrb = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);  // Instantiate new orb at firepoint position going forwards
            src.PlayOneShot(fireSound); // Play sound
            chargeSoundPlayed = false;
            chargeTrail.GetComponent<TrailRenderer>().emitting = false;
        }
        currentCharge = 0;  // Reset charge
        crosshairCharging.fillAmount = 0;
    }
}
