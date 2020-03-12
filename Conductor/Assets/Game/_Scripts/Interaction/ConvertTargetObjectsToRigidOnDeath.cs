using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertTargetObjectsToRigidOnDeath : MonoBehaviour
{
    [SerializeField] GameObject[] affectedObjects;
    [SerializeField] float objectMass = 1.0f;

    private void OnDestroy()
    {
        foreach(GameObject obj in affectedObjects)
        {
            if(!obj.GetComponent<Rigidbody>())
            {
                Rigidbody rb = obj.AddComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                rb.mass = objectMass;
            }
            else
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                rb.mass = objectMass;
            }
        }
    }
}
