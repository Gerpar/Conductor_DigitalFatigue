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
                Destroy(this.gameObject);
        }
    }

    public override void SetOnFire()
    {
        ParticleSystem.MainModule main;

        burning = true;
        flame = Instantiate(flame, this.transform.position, flame.transform.rotation);
        main = flame.main;
        main.duration = timeToDestroy;
        flame.Play();
    }
}
