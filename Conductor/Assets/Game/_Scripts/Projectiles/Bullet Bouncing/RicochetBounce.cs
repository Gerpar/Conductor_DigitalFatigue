using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBounce : MonoBehaviour
{
    [SerializeField] LayerMask collisionMask;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        collisionMask = 1 << collisionMask;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Time.deltaTime * gameObject.GetComponent<ProjectilePropulsion>().projectileVelocity + 0.5f, collisionMask))
        {
            if(ray.direction.x > 0.5)   // Going Right
            {
                if(hit.normal.x <= -0.5 && hit.normal.y <= -0.5)  // Top right bouncer
                {
                    transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);    // Go down
                }
                else if (hit.normal.x <= -0.5 && hit.normal.y >= 0.5)  // Bot right bouncer
                {
                    transform.eulerAngles = new Vector3(-90, transform.eulerAngles.y, 0);    // Go up
                }
            }
            else if(ray.direction.x < -0.5) // Going Left
            {
                if (hit.normal.x >= 0.5 && hit.normal.y <= 0.5)  // Top left bouncer
                {
                    transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);    // Go down
                }
                else if (hit.normal.x >= 0.5 && hit.normal.y >= 0.5)  // Bot left bouncer
                {
                    transform.eulerAngles = new Vector3(-90, transform.eulerAngles.y, 0);    // Go up
                }
            }
            else if(ray.direction.y > 0.5)  // Going up
            {
                if (hit.normal.x <= -0.5 && hit.normal.y <= -0.5)  // Top right bouncer
                {
                    transform.eulerAngles = new Vector3(-180, transform.eulerAngles.y, 0);    // Go right
                }
                else if (hit.normal.x >= 0.5 && hit.normal.y <= 0.5)  // Top left bouncer
                {
                    transform.eulerAngles = new Vector3(180, transform.eulerAngles.y, 0);  // Go left
                }
            }
            else if(ray.direction.y < -0.5) // Going down
            {
                if (hit.normal.x <= -0.5 && hit.normal.y >= 0.5)  // Bot right bouncer
                {
                    transform.eulerAngles = new Vector3(180, transform.eulerAngles.y, 0);  // Go left
                }
                else if (hit.normal.x >= 0.5 && hit.normal.y >= 0.5)  // Bot left bouncer
                {
                    transform.eulerAngles = new Vector3(-180, transform.eulerAngles.y, 0);    // Go right
                }
            }

            rb.velocity = transform.forward * rb.velocity.magnitude;

            //Debug.Log("Initial angle: " + transform.eulerAngles);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("BulletBouncer"))
        {
            Destroy(gameObject);
        }
    }
}
