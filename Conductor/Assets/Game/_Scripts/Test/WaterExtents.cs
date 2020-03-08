using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterExtents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float maxExtent = transform.localPosition.y + gameObject.GetComponent<Renderer>().bounds.extents.y;
        float minExtent = transform.localPosition.y - gameObject.GetComponent<Renderer>().bounds.extents.y;
        GameObject maxMarker;
        GameObject minMarker;

        Debug.Log(maxExtent);
        Debug.Log(minExtent);

        maxMarker = new GameObject("Max Extent");
        maxMarker.transform.position = this.transform.position;
        maxMarker.transform.parent = this.transform.parent;
        maxMarker.transform.Translate(new Vector3(0, gameObject.GetComponent<Renderer>().bounds.extents.y, 0));

        minMarker = new GameObject("Min Extent");
        minMarker.transform.position = this.transform.position;
        minMarker.transform.parent = this.transform.parent;
        minMarker.transform.Translate(new Vector3(0, -(gameObject.GetComponent<Renderer>().bounds.extents.y), 0));
    }
}
