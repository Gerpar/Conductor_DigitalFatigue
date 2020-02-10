using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas

public class ANDGate : ElectricObject
{
    public override void Activate()
    {
        bool newState = true;

        foreach (BaseMatter input in inputFrom)
        {
            newState &= input.IsElectrified;
        }

        if (newState != electrified)
        {
            electrified = newState;
            ActivateOutputs();
        }
    }
}
