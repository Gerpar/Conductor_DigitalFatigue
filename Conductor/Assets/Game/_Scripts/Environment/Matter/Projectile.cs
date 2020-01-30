using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas

public class Projectile : BaseMatter
{
    void Awake()
    {
        burning = true;
        electrified = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject triggeredObj = other.gameObject;
        BaseMatter material;

        // Checks if the object is part of the matter system
        if (triggeredObj.TryGetComponent(out material))
        {
            // Sets any flammable objects on fire if it is not already
            if (material.IsFlammable && !material.IsBurning)
                material.SetOnFire();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObj = collision.gameObject;
        BaseMatter material;

        // Ignores the collision if the object is not part of the matter system
        if (collidedObj.TryGetComponent(out material))
        {
            // Transfers electric charge if the object is conductive
            if (material.IsConductive)
                material.Activate();
        }
    }
}
