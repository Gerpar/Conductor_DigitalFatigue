using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateConduitOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Conduit"))  // Look for a conduit tag
        {
            collision.gameObject.GetComponent<ConduitControl>().TurnOn();   // Turn on the conduit
        }
    }
}
