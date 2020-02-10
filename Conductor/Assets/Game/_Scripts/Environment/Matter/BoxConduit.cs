using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas
// This script adds box conduit functionality to an object, meaning
// that a conductive object must be inside this object in order to
// complete the circuit

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(ConduitControl))]
public class BoxConduit : MonoBehaviour
{
    GameObject conductingObj;   // The object that has completed the circuit
    ConduitControl control;     // Activates other objects when the circuit is complete

    void Awake()
    {
        conductingObj = null;
        control = gameObject.GetComponent<ConduitControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // IMPORTANT : ConduitControl must be set to "Toggleable" for the box conduit to work
        if (control.Toggleable)
        {
            // Checks if the circuit is already complete
            if (conductingObj == null)
            {
                GameObject triggeredObj = other.gameObject;
                BaseMatter matter;

                // If the circuit is open, check if the incoming object is conductive
                if (triggeredObj.TryGetComponent(out matter) && matter.IsConductive)
                {
                    // If the incoming object is conductive, add it to the circuitt and turn on the objects in the ConduitControl script
                    control.TurnOn();
                    conductingObj = triggeredObj;
                }
            }
        }
        else
        {
            Debug.LogWarning("Conduit Controls attached to Box Conduits must be set to Toggleable in order to function");
        }
    }

    // This function is used to handle an edge case where the circuit has been broken, but another conductive object is still inside the box conduit
    private void OnTriggerStay(Collider other)
    {
        // Check if the conductive object has been removed
        if (control.Toggleable && conductingObj == null)
        {
            GameObject triggeredObj = other.gameObject;
            BaseMatter matter;

            // If the circuit has been broken, check if any of the remaining object(s) are conductive
            if (triggeredObj.TryGetComponent(out matter) && matter.IsConductive)
            {
                // If one of the objects is conductive, that object is used to complete the circuit
                control.TurnOn();
                conductingObj = triggeredObj;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (control.Toggleable)
        {
            // Check if the outgoing object was part of the circuit
            if (conductingObj != null && conductingObj == other.gameObject)
            {
                // If the object was part of the circuit, turn off the objects in ConduitControl
                control.TurnOff();
                conductingObj = null;
            }
        }
        else
        {
            Debug.LogWarning("Conduit Controls attached to Box Conduits must be set to Toggleable in order to function");
        }
    }


}
