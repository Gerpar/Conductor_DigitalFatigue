using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas

public class ConduitToggle : ElectricObject
{
    void Awake()
    {
        base.Awake();
        conductive = true;
    }

    public override void Activate()
    {
        electrified = !electrified;
        ActivateOutputs();
    }
}
