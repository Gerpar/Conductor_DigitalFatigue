using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : BaseMatter
{
    void Awake()
    {
        buoyant = true;
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
            onFire = CheckForFire(material);
        }
    }
}
