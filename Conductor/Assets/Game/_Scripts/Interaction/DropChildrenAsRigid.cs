using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropChildrenAsRigid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        foreach(Transform obj in transform)
        {
            obj.parent = null;

            if (!obj.GetComponent<Rigidbody>())
            {
                obj.gameObject.AddComponent<Rigidbody>();
            }
            
            if(obj.GetComponent<TurretController>())
            {
                obj.GetComponent<TurretController>().enabled = false;
            }
        }
    }
}
