using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] float fireDelay;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;


    bool turretOnline = false;

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

    IEnumerator AutoFire()
    {
        while(turretOnline)
        {
            GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation, null);
            yield return new WaitForSeconds(fireDelay);
        }
    }

    void Start()
    {
        StartCoroutine(AutoFire());
    }
}
