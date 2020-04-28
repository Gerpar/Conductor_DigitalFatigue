using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas
// This script marks any object it is attached to as flammable, and handles it's destruction once set on fire
// Slight edit by Gerad Paris to change the parent of children when destroyed

public class FlammableObject : BaseMatter
{
    public ParticleSystem flame;            // Prefab fire effect that plays while the object is burning 
    [SerializeField]
    protected float timeToDestroy = 0.0f;   // How long it takes for the fire to completely burn away this object
    [SerializeField] Transform newParent;  // Parent object that the children will be moved to

    protected float timeElapsed;    // How much time has elapsed since the object was lit on fire

    void Awake()
    {
        timeElapsed = 0.0f;
        flammable = true;
    }

    void Update()
    {
        // After the object is set on fire, it is destroyed once the time elapsed exceeds timeToDestroy
        if (burning)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= timeToDestroy)
            {
                if(newParent)
                {
                    foreach (Transform child in transform)
                    {
                        child.parent = newParent;
                    }
                }
                Destroy(this.gameObject);
            }
                
        }
    }

    public override void SetOnFire()
    {
        ParticleSystem.MainModule main;     // The main module of the fire particle effect

        burning = true;
        flame = Instantiate(flame, this.transform.position, flame.transform.rotation);
        main = flame.main;
        main.duration = timeToDestroy;  // The fire effect will disappear once the object is destroyed
        flame.Play();                   // (For some reason, the duration could not be set unless the main module was seperated from the particle system)
    }
}
