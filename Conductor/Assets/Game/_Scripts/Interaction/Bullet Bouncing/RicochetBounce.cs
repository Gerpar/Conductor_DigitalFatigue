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
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Time.deltaTime * gameObject.GetComponent<ProjectilePropulsion>().projectileVelocity + 0.5f, collisionMask))
        {
            Debug.Log("Bouncing");
            Debug.Log("Initial angle: " + transform.eulerAngles);
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = Mathf.Atan2(reflectDir.y, reflectDir.z) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(rot, 0, 0);
            rb.velocity = transform.right * rb.velocity.magnitude;

            Debug.Log("Initial angle: " + transform.eulerAngles);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletBouncer"))
        {
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
