using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePropulsion : MonoBehaviour
{
    public float projectileVelocity;
    [SerializeField] float projectileAcceleration;
    [SerializeField] float lifeTime; // How long until the projectile is destroyed
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<Rigidbody>() == null)    // If projectile doesn't have rigidbody, add it
        {
            gameObject.AddComponent<Rigidbody>();
        }

        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileVelocity;           // Set standard velocity to inputted variable

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward * projectileAcceleration);   // Accelerate object
    }
}
