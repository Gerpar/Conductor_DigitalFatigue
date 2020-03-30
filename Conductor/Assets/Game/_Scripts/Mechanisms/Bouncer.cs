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
                    Debug.Log(col.gameObject.name);
                    col.gameObject.GetComponent<Rigidbody>().velocity = launchForce;     // Launch based on the launch force
                }
        }
    }
}
