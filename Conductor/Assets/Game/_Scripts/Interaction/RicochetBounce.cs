using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBounce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BulletBouncer"))
        {
            Vector3 newNormal = Vector3.Reflect(gameObject.transform.forward, collision.contacts[0].normal);

            gameObject.transform.forward = Vector3.Reflect(gameObject.transform.forward, collision.contacts[0].normal);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
