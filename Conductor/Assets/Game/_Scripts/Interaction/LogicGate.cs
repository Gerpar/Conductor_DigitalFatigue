using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script created by Gerad paris
/// <summary>
/// Acts as the fundimental logic gates dependant on what type it is set as. Takes inputs from two conduits connected to it.
/// </summary>
/// 
public class LogicGate : ConduitControl
{
    enum GateType { AND, NAND };

    [SerializeField] GateType gateType;
    public GameObject conduitA, conduitB;

    Material gateOnMat, gateOffMat;

    Renderer rend;

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

    private void Start()
    {
        rend = GetComponent<Renderer>();
        gateOnMat = Resources.Load("Materials/Wiring/LogicGateOn") as Material;
        gateOffMat = Resources.Load("Materials/Wiring/LogicGateOff") as Material;
    }

    void CheckOutput()
    {
        switch(gateType)
        {
            case GateType.AND:
                if(inA && inB)  // In input A & B Are active
                {
                    conduitEnabled = true;
                    rend.material = gateOnMat;
                }
                else
                {
                    conduitEnabled = false;
                    rend.material = gateOffMat;
                }
                break;
            case GateType.NAND:
                if(!(inA && inB))   // If input A & B Are not active
                {
                    conduitEnabled = true;
                    rend.material = gateOnMat;
                }
                else
                {
                    conduitEnabled = false;
                    rend.material = gateOffMat;
                }
                break;
        }

        UpdateAllObjects();    // Update the objects
    }
}