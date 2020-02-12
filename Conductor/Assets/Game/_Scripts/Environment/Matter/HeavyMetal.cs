using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class HeavyMetal : BaseMatter
{
    // Start is called before the first frame update
    void Awake()
    {
        buoyant = false;
        conductive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
