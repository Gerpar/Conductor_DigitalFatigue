using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script created by Gerad paris
/// <summary>
/// Controls the turret's firing patterns, and wheter or not it will track the player's movements. Turret also attempts to predict the player's movement.
/// </summary>

public class TurretController : MonoBehaviour
{
    [Header("Standard Properties")]
    [SerializeField] float fireDelay;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip detectionSound;

    [Header("Tracking Properties")]
    [SerializeField] bool tracking;
    [SerializeField] float rotationSpeed;

    GameObject playerObj;
    bool turretOnline = false;

    bool playerDetected = false;
    AudioSource src;
    bool coroutineStarted = false;

    public bool TurretState // Sets the state of the turret, and activates / deactivates coroutines based on the value passed
    {
        set
        {
            turretOnline = value;
            if (turretOnline && !tracking)
            {
                StartCoroutine(AutoFire());
            }
            else
            {
                StopAllCoroutines();
                coroutineStarted = false;
            }
        }
    }

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player"); // Find the player object
        src = GetComponent<AudioSource>();                      // Get audio source
    }

    void Update()
    {
        if (playerDetected && turretOnline && tracking)
        {
            PointTowardsPoint(playerObj.transform.position + (playerObj.GetComponent<Rigidbody>().velocity * Time.deltaTime * Vector3.Distance(transform.position, playerObj.transform.position))); // Aim ahead of the player's movement
        }
    }

    // Controls the fire rate of the turret (How often it fires)
    IEnumerator AutoFire()
    {
        coroutineStarted = true;

        yield return new WaitForSeconds(0.25f);
        src.PlayOneShot(detectionSound);

        yield return new WaitForSeconds(fireDelay);         // Wait one shot before firing, gives turret time to aim
        while (turretOnline)
        {
            if (!tracking)  // If turret isn't tracking, fire a projectile
            {
                GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation, null);
                src.PlayOneShot(fireSound);
            }
            else if (playerDetected)    // If player is detected, fir a projectile
            {
                GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation, null);
                src.PlayOneShot(fireSound);
            }

            yield return new WaitForSeconds(fireDelay);
        }
    }

    // Aims towards a given point, will aim towards the player on a predicted path
    void PointTowardsPoint(Vector3 point)
    {
        Vector3 direction = (point - transform.position).normalized;            // Get the direction towards the point

        Quaternion lookRotation = Quaternion.LookRotation(direction);           // Create a quaternion for the new look rotation

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);  // SLerp towards the new look position from the current rotation.
    }

    // Detection triggers, pretty self explanitory.
    private void OnTriggerStay(Collider other)
    {
        if (tracking && turretOnline && !coroutineStarted)
        {
            if (other.CompareTag("Player"))
            {
                
                StartCoroutine(AutoFire());
                playerDetected = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tracking)
        {
            if (other.CompareTag("Player"))
            {
                StopAllCoroutines();
                playerDetected = false;
                coroutineStarted = false;
            }
        }
    }
}
