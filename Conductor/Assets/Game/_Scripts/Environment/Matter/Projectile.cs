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

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObj = collision.gameObject;
        BaseMatter material;

        if (gameObject.TryGetComponent(out material))
        {
            if(material.IsFlammable)
                material.SetOnFire();
        }
    }
}
