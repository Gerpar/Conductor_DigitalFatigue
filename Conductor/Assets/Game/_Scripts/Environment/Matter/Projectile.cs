using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseMatter
{
    void Awake()
    {
        burning = true;
        electrified = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            GameObject triggeredObj = other.gameObject;
            BaseMatter material;

            if (triggeredObj.TryGetComponent(out material))
            {
                if (material.IsFlammable && !material.IsBurning)
                    material.SetOnFire();
            }
        }
    }

}
