using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas
// This script is applied to objects attached to a rope in order to update the physics system
// (Without this script, the object will simply hover in the air when the rope is destroyed)

public class CheckSuspension : MonoBehaviour
{
    private FixedJoint joint;

    // Start is called before the first frame update
    void Awake()
    {
        joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        // Once the rigidbody this object is attached to is destroyed, the hinge joint script is removed and this script is disabled
        if (joint != null && joint.connectedBody == null)
        {
            Destroy(joint);
            enabled = false;
        }
    }
}
