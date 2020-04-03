using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas

public class Door : ElectricObject
{
    [SerializeField]
    private bool isClosed = true;

    void Awake()
    {
        base.Awake();
        this.gameObject.SetActive(isClosed);
    }

    public override void Activate()
    {
        isClosed = !isClosed;
        electrified = !isClosed;
        this.gameObject.SetActive(isClosed);
    }
}
