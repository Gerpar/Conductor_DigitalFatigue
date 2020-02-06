using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas
// The base class for the systemic gameplay loop

public abstract class BaseMatter : MonoBehaviour
{
    public bool IsBurning { get { return burning; } }           // Whether the object is on fire
    public bool IsElectrified { get { return electrified; } }   // Whether the oject is currently conducting electricity
    public bool IsFlammable { get { return flammable; } }       // Whether the object is able to be set on fire
    public bool IsConductive { get { return conductive; } }     // Whether the object is capable of conducting electricity
    public bool IsBuoyant { get { return buoyant; } }           // Whether the object floats in water/acid

    protected bool burning = false;
    protected bool electrified = false;

    protected bool flammable = false;
    protected bool conductive = false;
    protected bool buoyant = false;

    public virtual void SetOnFire()
    {
        burning = true;
    }

    public virtual void Activate()
    {

    }

    public virtual void Rise()
    {

    }
}
