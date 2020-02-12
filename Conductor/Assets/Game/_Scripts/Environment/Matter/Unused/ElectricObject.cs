using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas

public class ElectricObject : BaseMatter
{
    public List<ElectricObject> outputTo;
    [HideInInspector]
    public List<ElectricObject> inputFrom;

    public void Awake()
    {
        foreach (ElectricObject output in outputTo)
        {
            output.inputFrom.Add(this);
        }
    }

    public void ActivateOutputs()
    {
        foreach (BaseMatter output in outputTo)
        {
            output.Activate();
        }
    }
}
