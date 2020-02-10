﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConduitControl : MonoBehaviour
{
    [SerializeField] GameObject[] effectedObjects;
    [SerializeField] Material wireOnMaterial, wireOffMaterial;
    [SerializeField] bool toggleable;

    bool conduitEnabled;    // if the conduit is enabled

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
        foreach(GameObject obj in effectedObjects)  // Check each object in the array
        {
            if(obj.GetComponent<ObjectEffect>() == null)    // If object doesn't have an effect
            {
                Debug.Log("Error, " + obj.name + " does not have an effect! Generating default.");
                obj.AddComponent<ObjectEffect>();   // Add a standard component
            }
        }
        UpdateObjects();    // Update the objects to their current states
    }

    // Turns on the conduit
    public void TurnOn()
    {
        conduitEnabled = true;
        UpdateObjects();    // update the attached objects
    }

    // Turns on the conduit
    public void TurnOff()
    {
        conduitEnabled = false;
        UpdateObjects();    // Update the attached objects
    }

    private void UpdateObjects()
    {
        foreach(GameObject obj in effectedObjects)  // For every object the conduit affects
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
                    if(conduitEnabled)  // If conduit is on, turn on wire
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
                    if(conduitEnabled)  // If conduit is on, extend the piston
                        obj.GetComponent<Piston>().Extend();
                    else                // If conduit os off, retract the piston
                        obj.GetComponent<Piston>().Retract();
                    break;
                case ObjectEffect.EffectType.PISTON_RETRACT:
                    if(conduitEnabled)  // If conduit is on, retract the piston
                        obj.GetComponent<Piston>().Retract();
                    else                // If conduit is off, extend the piston
                        obj.GetComponent<Piston>().Extend();
                    break;

                // Gates
                //--------------------
                case ObjectEffect.EffectType.LOGIC_GATE:      // AND
                    LogicGate gate = obj.GetComponent<LogicGate>();
                    if(gate.conduitA == gameObject) // Check if this conduit is conduit A
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
                    obj.GetComponent<Bouncer>().enabled = !conduitEnabled;
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
}
