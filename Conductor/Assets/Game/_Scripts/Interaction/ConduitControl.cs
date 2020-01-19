using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConduitControl : MonoBehaviour
{
    [SerializeField] GameObject[] effectedObjects;
    [SerializeField] Material wireOnMaterial, wireOffMaterial;

    bool conduitEnabled;    // if the conduit is enabled

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

        UpdateObjects();
    }

    // Turns on the conduit
    public void TurnOn()
    {
        conduitEnabled = true;
        UpdateObjects();
    }

    // Turns on the conduit
    public void TurnOff()
    {
        conduitEnabled = false;
        UpdateObjects();
    }

    private void UpdateObjects()
    {
        foreach(GameObject obj in effectedObjects)  // For every object the conduit affects
        {
            ObjectEffect.EffectType effect = obj.GetComponent<ObjectEffect>().effect;   // Get the effect that the conduit should have on the object

            switch(effect)  // Do a switch on that effect to quickly find the effect
            {
                case ObjectEffect.EffectType.DOOR_CLOSE:    // Door opens when signal is revieved
                    obj.SetActive(conduitEnabled);
                    break;
                case ObjectEffect.EffectType.DOOR_OPEN:     // Door closes when signal is recieved
                    obj.SetActive(!conduitEnabled);
                    break;
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
            }
        }
    }
}
