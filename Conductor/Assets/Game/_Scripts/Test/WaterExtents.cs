using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterExtents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position.y + gameObject.GetComponent<Renderer>().bounds.extents.y);
    }
}
