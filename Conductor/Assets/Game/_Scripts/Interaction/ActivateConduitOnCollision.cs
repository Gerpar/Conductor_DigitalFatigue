using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateConduitOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Conduit"))  // Look for a conduit tag
        {
            ConduitControl condControl = collision.gameObject.GetComponent<ConduitControl>();

            if (!condControl.Toggleable)
            {
                condControl.TurnOn();   // Turn on the conduit
            }
            else
            {
                if(condControl.ConduitEnabled)
                {
                    condControl.TurnOff();
                }
                else
                {
                    condControl.TurnOn();
                }
            }
        }
    }

    
}
