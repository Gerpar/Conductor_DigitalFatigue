using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderbussController : MonoBehaviour
{
    [Header("General Properties")] 
    [SerializeField] Transform thunderbussHolderTransform;
    [SerializeField] Camera camera;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject chargeTrail;

    [Header("Projectile properties")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float chargeTime;   // Seconds it takes for thunderbuss to charge a shot

    [Header("Audio")]
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip chargeSound;

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
        chargeTrail.SetActive(false);
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
        crosshair.transform.position = Input.mousePosition;

        mousePosInWorld = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -camera.transform.position.z));

        mousePosInWorld.z = thunderbussHolderTransform.position.z;

        Debug.Log(mousePosInWorld);
    }

    void Charging()
    {
        currentCharge += Time.deltaTime;
        if(currentCharge >= chargeTime && !chargeSoundPlayed)
        {
            src.PlayOneShot(chargeSound);
            chargeSoundPlayed = true;
            chargeTrail.SetActive(true);
        }
    }

    void Firing()
    {
        if(currentCharge >= chargeTime)
        {
            GameObject newOrb = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);  // Instantiate new orb at firepoint position going forwards
            src.PlayOneShot(fireSound); // Play sound
            chargeSoundPlayed = false;
            chargeTrail.SetActive(false);
        }
        currentCharge = 0;  // Reset charge
    }
}
