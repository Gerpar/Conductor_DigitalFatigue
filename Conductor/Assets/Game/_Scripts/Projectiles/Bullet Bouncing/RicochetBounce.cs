using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBounce : MonoBehaviour
{
    [SerializeField] LayerMask collisionMask;
    [SerializeField] GameObject collisionParticles;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Time.deltaTime * gameObject.GetComponent<ProjectilePropulsion>().projectileVelocity + 0.5f, collisionMask))
        {
            Debug.Log("Ray Direction: " + ray.direction);
            Debug.Log("Hit Normal:    " + hit.normal.x);

            float rotationVal = transform.eulerAngles.y;
            float absoluteRotationY = rotationVal == 270 ? 90 : rotationVal;    // Make sure that the bouncer workes regardles of what direction the player was facing.

            if (ray.direction.x > 0.5)   // Going Right
            {
                if (hit.normal.x <= -0.5 && hit.normal.y <= -0.5)  // Top right bouncer
                {
                    transform.eulerAngles = new Vector3(90, absoluteRotationY, 0);    // Go down
                }
                else if (hit.normal.x <= -0.5 && hit.normal.y >= 0.5)  // Bot right bouncer
                {
                    transform.eulerAngles = new Vector3(-90, absoluteRotationY, 0);    // Go up
                }
            }
            else if (ray.direction.x < -0.5) // Going Left
            {
                if (hit.normal.x >= 0.5 && hit.normal.y <= 0.5)  // Top left bouncer
                {
                    transform.eulerAngles = new Vector3(90, absoluteRotationY, 0);    // Go down
                }
                else if (hit.normal.x >= 0.5 && hit.normal.y >= 0.5)  // Bot left bouncer
                {
                    transform.eulerAngles = new Vector3(-90, absoluteRotationY, 0);    // Go up
                }
            }
            else if (ray.direction.y > 0.5)  // Going up
            {
                if (hit.normal.x <= -0.5 && hit.normal.y <= -0.5)  // Top right bouncer
                {
                    transform.eulerAngles = new Vector3(180, absoluteRotationY, 0);  // Go left
                }
                else if (hit.normal.x >= 0.5 && hit.normal.y <= 0.5)  // Top left bouncer
                {
                    transform.eulerAngles = new Vector3(0, absoluteRotationY, 0);    // Go right
                }
            }
            else if (ray.direction.y < -0.5) // Going down
            {
                if (hit.normal.x <= -0.5 && hit.normal.y >= 0.5)  // Bot right bouncer
                {
                    transform.eulerAngles = new Vector3(180, absoluteRotationY, 0);  // Go left
                }
                else if (hit.normal.x >= 0.5 && hit.normal.y >= 0.5)  // Bot left bouncer
                {
                    transform.eulerAngles = new Vector3(-180, absoluteRotationY, 0);    // Go right
                }
            }

            rb.velocity = transform.forward * rb.velocity.magnitude;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("BulletBouncer"))
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.CompareTag("KeepChildAfterDeath"))
                {
                    child.transform.SetParent(null);
                    if (child.gameObject.GetComponent<ParticleSystem>())
                    {
                        child.gameObject.GetComponent<ParticleSystem>().Stop();
                    }
                    if (child.gameObject.GetComponent<TrailRenderer>())
                    {
                        child.gameObject.GetComponent<TrailRenderer>().emitting = false;
                    }
                }
                Destroy(child.gameObject, 5.0f);
            }

            GameObject newParticles = Instantiate(collisionParticles, transform.position, transform.rotation, null);
            Destroy(newParticles, 2.0f);
            Destroy(gameObject);
        }
    }
}
