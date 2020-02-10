using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGate : ConduitControl
{
    enum GateType { AND, NAND };

    [SerializeField] GateType gateType;
    public GameObject conduitA, conduitB;

    private bool inA, inB;

    public bool InA
    {
        get { return inA; }
        set
        {
            inA = value;
            CheckOutput();
        }
    }

    public bool InB
    {
        get { return inB; }
        set
        {
            inB = value;
            CheckOutput();
        }
    }

    public bool Output
    {
        get
        {
            return conduitEnabled;
        }
    }
    void CheckOutput()
    {
        switch(gateType)
        {
            case GateType.AND:
                if(inA && inB)  // In input A & B Are active
                {
                    conduitEnabled = true;
                }
                else
                {
                    conduitEnabled = false;
                }
                break;
            case GateType.NAND:
                if(!(inA && inB))   // If input A & B Are not active
                {
                    conduitEnabled = true;
                }
                else
                {
                    conduitEnabled = false;
                }
                break;
        }

        UpdateObjects();    // Update the objects
    }
}
