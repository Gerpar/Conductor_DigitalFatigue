using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas

public class ConduitPermanent : ElectricObject
{
    void Awake()
    {
        base.Awake();
        conductive = true;
    }

    public override void Activate()
    {
        if (!electrified)
        {
            electrified = true;
            ActivateOutputs();
        }
    }
}
