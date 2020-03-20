using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] string[] effectedTags = { "Player" };  // Compare with player by default
    [SerializeField] Vector3 launchForce = new Vector3(0, 10, 0);
    [SerializeField] bool enabled = true;

    public bool Enabled
    {
        get { return enabled; }
        set { enabled = value; }
    }

    void OnCollisionStay(Collision col)
    {
        if(enabled) // If bouncer is enabled
        {
            foreach (string tag in effectedTags)    // Check each effected tag
                if (col.transform.tag == tag)       // If the tag matches one of the effected tags
                {
                    Vector3 rbVel = col.gameObject.GetComponent<Rigidbody>().velocity;
                    Vector3 newVel = new Vector3(rbVel.x + launchForce.x, launchForce.y, rbVel.z + launchForce.z);
                    col.gameObject.GetComponent<Rigidbody>().velocity = newVel;     // Launch based on the launch force
                }
        }
    }
}
