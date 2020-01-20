using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    [SerializeField] float pistonMoveSpeed;
    [SerializeField] float extensionHeight;
    [SerializeField] GameObject extensionObject;

    private bool finishedMoving = true;
    private bool extending = false;
    private Rigidbody rb;

    private Vector3 finalPosition, initialPosition;
    void Start()
    {
        rb = extensionObject.GetComponent<Rigidbody>();
        finalPosition = new Vector3(extensionObject.transform.position.x, extensionObject.transform.position.y + extensionHeight, extensionObject.transform.position.z);
        initialPosition = new Vector3(extensionObject.transform.position.x, 0, extensionObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!finishedMoving)
        {
            if(extending)
            {
                rb.velocity = new Vector3(0, pistonMoveSpeed, 0);
                if(extensionObject.transform.position.y >= finalPosition.y)
                {
                    finishedMoving = true;
                    extensionObject.transform.position = finalPosition;
                    rb.velocity = Vector3.zero;
                }
            }
            else
            {
                rb.velocity = new Vector3(0, -pistonMoveSpeed, 0);
                if (extensionObject.transform.position.y <= initialPosition.y)
                {
                    finishedMoving = true;
                    extensionObject.transform.position = initialPosition;
                    rb.velocity = Vector3.zero;
                }
            }
        }
    }

    public void Extend()
    {
        finishedMoving = false;
        extending = true;
    }

    public void Retract()
    {
        finishedMoving = false;
        extending = false;
    }

}
