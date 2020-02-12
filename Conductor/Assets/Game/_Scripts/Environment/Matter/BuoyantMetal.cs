using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas
// Description

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class BuoyantMetal : BaseMatter
{
    // [SerializeField]
    // private Vector3 floatForce;
    // 
    // private bool inWater = false;
    // private GameObject medium;

    void Awake()
    {
        buoyant = true;
        // GetComponent<BoxCollider>().isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        // The plan (TM) :
        // 1) Determine if this object is in water via OnCollisionEnter()
        // 2) If in water, set in Water to True copy the water object to medium
        // 3) In the update function, determine how close to the top of the water this object is
        // 4) Apply progressively less force as the object reaches the top
    }

    // public override void Rise()
    // {
    //     base.Rise();
    // }
    // 
    // private void OnCollisionEnter(Collision collision)
    // {
    //     GameObject collidedObj = collision.gameObject;
    // 
    //     if (collidedObj.tag == "Water" && !inWater)
    //     {
    //         inWater = true;
    //         medium = collidedObj;
    //     }
    //         
    // }
    // 
    // private void OnCollisionExit(Collision collision)
    // {
    //     GameObject collidedObj = collision.gameObject;
    // 
    //     if (collidedObj.tag == "Water" && inWater)
    //     {
    //         inWater = false;
    //         medium = null;
    //     }
    // }
}
