using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableObject : BaseMatter
{
    public ParticleSystem flame;

    void Awake()
    {
        flammable = true;
        flame.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (burning)
        {

        }
    }

    public override void SetOnFire()
    {
        burning = true;
        flame.Play();
    }
}
