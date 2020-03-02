using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script created by Gerad paris
/// <summary>
/// Controls the objects that are attached to the conduit based on the current conduit status. Status and check are only done when an object hits it that has the ActivateConduitOnCollision script attached.
/// </summary>

public class ConduitControl : MonoBehaviour
{
    [SerializeField] GameObject[] effectedObjects;
    [SerializeField] bool toggleable;

    public bool conduitEnabled;    // if the conduit is enabled
    Material wireOnMaterial, wireOffMaterial;

    public bool Toggleable
    {
        get { return toggleable; }
    }

    public bool ConduitEnabled
    {
        get { return conduitEnabled; }
    }

    void Start()
    {
        wireOnMaterial = Resources.Load("Materials/Wiring/WireOn") as Material;
        wireOffMaterial = Resources.Load("Materials/Wiring/WireOff") as Material;

        foreach(GameObject obj in effectedObjects)  // Check each object in the array
        {
            ObjectEffect effectObj = obj.GetComponent<ObjectEffect>();

            if (effectObj == null)    // If object doesn't have an effect
            {
                Debug.Log("Error, " + obj.name + " does not have an effect! Generating default.");
                obj.AddComponent<ObjectEffect>();   // Add a standard component
            }
            // Update all objects except water to its default states
            else if (effectObj.effect != ObjectEffect.EffectType.WATER_LEVEL)
            {
                UpdateObject(obj);
            }
        }
        // UpdateObjects();    // Update the objects to their current states
    }

    // Turns on the conduit
    public void TurnOn()
    {
        conduitEnabled = true;
        // UpdateObjects();    // update the attached objects
        UpdateAllObjects();
    }

    // Turns on the conduit
    public void TurnOff()
    {
        conduitEnabled = false;
        // UpdateObjects();    // Update the attached objects
        UpdateAllObjects();
    }

    // private void UpdateObjects()
    // {
    //     foreach(GameObject obj in effectedObjects)  // For every object the conduit affects
    //     {
    //         ObjectEffect.EffectType effect = obj.GetComponent<ObjectEffect>().effect;   // Get the effect that the conduit should have on the objectd
    // 
    //         switch (effect)  // Do a switch on that effect to quickly find the effect
    //         {
    //             // Doors
    //             //--------------------
    //             case ObjectEffect.EffectType.DOOR_CLOSE:    // Door opens when signal is revieved
    //                 obj.SetActive(conduitEnabled);
    //                 break;
    //             case ObjectEffect.EffectType.DOOR_OPEN:     // Door closes when signal is recieved
    //                 obj.SetActive(!conduitEnabled);
    //                 break;
    // 
    //             // Wires
    //             //--------------------
    //             case ObjectEffect.EffectType.WIRE_ON:
    //                 if(conduitEnabled)  // If conduit is on, turn on wire
    //                     obj.GetComponent<Renderer>().material = wireOnMaterial;
    //                 else                // If conduit is off, turn off wire
    //                     obj.GetComponent<Renderer>().material = wireOffMaterial;
    //                 break;
    //             case ObjectEffect.EffectType.WIRE_OFF:
    //                 if (conduitEnabled)  // If conduit is on, turn off wire
    //                     obj.GetComponent<Renderer>().material = wireOffMaterial;
    //                 else                // If conduit is off, turn on wire
    //                     obj.GetComponent<Renderer>().material = wireOnMaterial;
    //                 break;
    // 
    //             // Pistons
    //             //--------------------
    //             case ObjectEffect.EffectType.PISTON_EXTEND:
    //                 if(conduitEnabled)  // If conduit is on, extend the piston
    //                     obj.GetComponent<Piston>().Extend();
    //                 else                // If conduit os off, retract the piston
    //                     obj.GetComponent<Piston>().Retract();
    //                 break;
    //             case ObjectEffect.EffectType.PISTON_RETRACT:
    //                 if(conduitEnabled)  // If conduit is on, retract the piston
    //                     obj.GetComponent<Piston>().Retract();
    //                 else                // If conduit is off, extend the piston
    //                     obj.GetComponent<Piston>().Extend();
    //                 break;
    // 
    //             // Gates
    //             //--------------------
    //             case ObjectEffect.EffectType.LOGIC_GATE:      // AND
    //                 LogicGate gate = obj.GetComponent<LogicGate>();
    //                 if(gate.conduitA == gameObject) // Check if this conduit is conduit A
    //                 {
    //                     gate.InA = conduitEnabled;
    //                 }
    //                 else                            // This conduit is conduit B
    //                 {
    //                     gate.InB = conduitEnabled;
    //                 }
    //                 break;
    // 
    //             // Bouncers
    //             //--------------------
    //             case ObjectEffect.EffectType.BOUNCER:
    //                 obj.GetComponent<Bouncer>().Enabled = conduitEnabled;
    //                 break;
    //             case ObjectEffect.EffectType.BOUNCER_INV:
    //                 obj.GetComponent<Bouncer>().enabled = !conduitEnabled;
    //                 break;
    // 
    //             // Water Level Change
    //             //--------------------
    //             case ObjectEffect.EffectType.WATER_LEVEL:
    //                 WaterLevel level = obj.GetComponent<WaterLevel>();
    //                 level.waterBody.ChangeWaterLevel(level.waterHeight);
    //                 break;
    //         }
    //     }
    // }

    // Activates every object connected to this conduit
    protected void UpdateAllObjects()
    {
        foreach (GameObject go in effectedObjects)
        {
            UpdateObject(go);
        }
    }


    // Activates a specific object connect to this conduit
    protected void UpdateObject(GameObject obj)
    {
        ObjectEffect.EffectType effect = obj.GetComponent<ObjectEffect>().effect;   // Get the effect that the conduit should have on the objectd

        switch (effect)  // Do a switch on that effect to quickly find the effect
        {
            // Doors
            //--------------------
            case ObjectEffect.EffectType.DOOR_CLOSE:    // Door opens when signal is revieved
                obj.SetActive(conduitEnabled);
                break;
            case ObjectEffect.EffectType.DOOR_OPEN:     // Door closes when signal is recieved
                obj.SetActive(!conduitEnabled);
                break;

            // Wires
            //--------------------
            case ObjectEffect.EffectType.WIRE_ON:
                if (conduitEnabled)  // If conduit is on, turn on wire
                    obj.GetComponent<Renderer>().material = wireOnMaterial;
                else                // If conduit is off, turn off wire
                    obj.GetComponent<Renderer>().material = wireOffMaterial;
                break;
            case ObjectEffect.EffectType.WIRE_OFF:
                if (conduitEnabled)  // If conduit is on, turn off wire
                    obj.GetComponent<Renderer>().material = wireOffMaterial;
                else                // If conduit is off, turn on wire
                    obj.GetComponent<Renderer>().material = wireOnMaterial;
                break;

            // Pistons
            //--------------------
            case ObjectEffect.EffectType.PISTON_EXTEND:
                if (conduitEnabled)  // If conduit is on, extend the piston
                    obj.GetComponent<Piston>().Extend();
                else                // If conduit os off, retract the piston
                    obj.GetComponent<Piston>().Retract();
                break;
            case ObjectEffect.EffectType.PISTON_RETRACT:
                if (conduitEnabled)  // If conduit is on, retract the piston
                    obj.GetComponent<Piston>().Retract();
                else                // If conduit is off, extend the piston
                    obj.GetComponent<Piston>().Extend();
                break;

            // Gates
            //--------------------
            case ObjectEffect.EffectType.LOGIC_GATE:      // AND
                LogicGate gate = obj.GetComponent<LogicGate>();
                if (gate.conduitA == gameObject) // Check if this conduit is conduit A
                {
                    gate.InA = conduitEnabled;
                }
                else                            // This conduit is conduit B
                {
                    gate.InB = conduitEnabled;
                }
                break;

            // Bouncers
            //--------------------
            case ObjectEffect.EffectType.BOUNCER:
                obj.GetComponent<Bouncer>().Enabled = conduitEnabled;
                break;
            case ObjectEffect.EffectType.BOUNCER_INV:
                obj.GetComponent<Bouncer>().Enabled = !conduitEnabled;
                break;

            // Water Level Change
            //--------------------
            case ObjectEffect.EffectType.WATER_LEVEL:
                WaterLevel level = obj.GetComponent<WaterLevel>();
                level.waterBody.ChangeWaterLevel(level.waterHeight);
                break;
        }
    }
}
