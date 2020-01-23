using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMatter : MonoBehaviour
{
    public bool IsOnFire { get { return onFire; } }
    public bool IsConducting { get { return conducting; } }
    public bool IsBuoyant { get { return buoyant; } }

    protected bool onFire = false;
    protected bool conducting = false;
    protected bool buoyant = false;
}
