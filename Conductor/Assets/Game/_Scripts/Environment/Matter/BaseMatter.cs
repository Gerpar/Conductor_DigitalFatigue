using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMatter : MonoBehaviour
{
    public bool IsBurning { get { return burning; } }
    public bool IsElectrified { get { return electrified; } }
    public bool IsFlammable { get { return flammable; } }
    public bool IsConductive { get { return conductive; } }
    public bool IsBuoyant { get { return buoyant; } }

    protected bool burning = false;
    protected bool electrified = false;

    protected bool flammable = false;
    protected bool conductive = false;
    protected bool buoyant = false;

    public virtual void SetOnFire()
    {
        burning = true;
    }
}
