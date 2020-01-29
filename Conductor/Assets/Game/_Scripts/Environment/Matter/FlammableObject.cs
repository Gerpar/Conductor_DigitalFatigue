using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableObject : BaseMatter
{
    public ParticleSystem flame;
    [SerializeField]
    private float timeToDestroy = 0.0f;

    private float timeElapsed;

    void Awake()
    {
        timeElapsed = 0.0f;
        flammable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (burning)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= timeToDestroy)
                Debug.Log("ITS OVER");
        }
    }

    public override void SetOnFire()
    {
        burning = true;
        Instantiate(flame, this.transform);
    }
}
