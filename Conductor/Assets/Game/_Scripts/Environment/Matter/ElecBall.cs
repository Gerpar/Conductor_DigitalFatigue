using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecBall : BaseMatter
{
    // Start is called before the first frame update
    void Start()
    {
        onFire = true;
        conducting = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
