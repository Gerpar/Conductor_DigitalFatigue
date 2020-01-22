using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGate : MonoBehaviour
{
    enum GateType { AND, NAND };

    [SerializeField] GateType gateType;
    [SerializeField] GameObject[] effectedObjects;
    public GameObject conduitA, conduitB;
    [SerializeField] Material wireOnMaterial, wireOffMaterial;

    private bool inA, inB;
    private bool output;

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
            return output;
        }
    }
    void CheckOutput()
    {
        switch(gateType)
        {
            case GateType.AND:
                if(inA && inB)  // In input A & B Are active
                {
                    output = true;
                }
                else
                {
                    output = false;
                }
                break;
            case GateType.NAND:
                if(!(inA && inB))   // If input A & B Are not active
                {
                    output = true;
                }
                else
                {
                    output = false;
                }
                break;
        }

        UpdateObjects();    // Update the objects
    }

    private void UpdateObjects()
    {
        foreach (GameObject obj in effectedObjects)  // For every object the gate affects
        {
            ObjectEffect.EffectType effect = obj.GetComponent<ObjectEffect>().effect;   // Get the effect that the gate should have on the object

            switch (effect)  // Do a switch on that effect to quickly find the effect
            {
                case ObjectEffect.EffectType.DOOR_CLOSE:    // Door opens when signal is revieved
                    obj.SetActive(output);
                    break;
                case ObjectEffect.EffectType.DOOR_OPEN:     // Door closes when signal is recieved
                    obj.SetActive(!output);
                    break;
                case ObjectEffect.EffectType.WIRE_ON:
                    if (output)         // If gate is on, turn on wire
                        obj.GetComponent<Renderer>().material = wireOnMaterial;
                    else                // If gate is off, turn off wire
                        obj.GetComponent<Renderer>().material = wireOffMaterial;
                    break;
                case ObjectEffect.EffectType.WIRE_OFF:
                    if (output)         // If gate is on, turn off wire
                        obj.GetComponent<Renderer>().material = wireOffMaterial;
                    else                // If gate is off, turn on wire
                        obj.GetComponent<Renderer>().material = wireOnMaterial;
                    break;
                case ObjectEffect.EffectType.PISTON_EXTEND:
                    if (output)         // If gate is on, extend piston
                        obj.GetComponent<Piston>().Extend();
                    else                // If gate is off, retract piston
                        obj.GetComponent<Piston>().Retract();
                    break;
                case ObjectEffect.EffectType.PISTON_RETRACT:
                    if (output)         // If gate is on, retract piston
                        obj.GetComponent<Piston>().Retract();
                    else                // If gate is off, extend piston
                        obj.GetComponent<Piston>().Extend();
                    break;
                case ObjectEffect.EffectType.LOGIC_GATE:      // AND
                    LogicGate gate = obj.GetComponent<LogicGate>();
                    if (gate.conduitA == gameObject) // Check if this gate is "conduit" A
                    {
                        gate.InA = output;
                    }
                    else                            // This gate is "conduit" B
                    {
                        gate.InB = output;
                    }
                    break;
            }
        }
    }
}
