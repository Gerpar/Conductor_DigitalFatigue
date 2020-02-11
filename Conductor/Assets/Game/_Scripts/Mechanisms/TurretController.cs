using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Standard Properties")]
    [SerializeField] float fireDelay;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;

    [Header("Tracking Properties")]
    [SerializeField] bool tracking;
    [SerializeField] float rotationSpeed;

    GameObject playerObj;
    bool turretOnline = false;

    bool playerDetected = false;
    AudioSource src;

    public bool TurretState
    {
        set
        {
            turretOnline = value;
            if (turretOnline)
                StartCoroutine(AutoFire());
            else
                StopAllCoroutines();
        }
    }

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        src = GetComponent<AudioSource>();
        StartCoroutine(AutoFire());
    }

    void Update()
    {
        if (playerDetected && turretOnline && tracking)
        {
            PointTowardsPoint(playerObj.transform.position + (playerObj.GetComponent<Rigidbody>().velocity * Time.deltaTime * Vector3.Distance(transform.position, playerObj.transform.position)));
        }
    }

    IEnumerator AutoFire()
    {
        yield return new WaitForSeconds(fireDelay);
        while (turretOnline)
        {
            if (!tracking)
            {
                GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation, null);

            }
            else if (playerDetected)
            {
                GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation, null);
            }

            yield return new WaitForSeconds(fireDelay);
        }
    }

    void PointTowardsPoint(Vector3 point)
    {
        Vector3 direction = (point - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Debug.Log(lookRotation.z);

        GetComponent<Rigidbody>().transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }
}
