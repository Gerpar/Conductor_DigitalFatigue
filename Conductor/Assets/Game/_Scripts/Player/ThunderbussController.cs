using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderbussController : MonoBehaviour
{
    [Header("General Properties")] 
    [SerializeField] Transform thunderbussHolderTransform;
    [SerializeField] Camera camera;
    [SerializeField] Transform firePoint;

    [Header("Projectile properties")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float rechargeDelay;   // Seconds it takes for thunderbuss to recharge
    [SerializeField] AudioClip fireSound;

    private Vector3 mousePosInWorld;    // Keeps track of the mouse position in the world
    private float timeToFire;           // Keeps track of what time the thunderbuss can fire again
    private AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        src = firePoint.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosInWorld();

        thunderbussHolderTransform.LookAt(mousePosInWorld); // Aim thunderbuss at mouse position

        if (Input.GetButtonDown("Fire"))
        {
            Firing();
        }

    }

    void GetMousePosInWorld()
    {
        mousePosInWorld = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -camera.transform.position.z));

        mousePosInWorld.z = thunderbussHolderTransform.position.z;

        Debug.Log(mousePosInWorld);
    }

    void Firing()
    {
        GameObject newOrb = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        src.PlayOneShot(fireSound);
    }
}
