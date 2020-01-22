using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    [SerializeField] float pistonMoveSpeed;
    [SerializeField] float extensionHeight;
    [SerializeField] GameObject extensionObject;

    private bool extending = false;
    private bool isMoving = false;
    private Rigidbody rb;

    private Vector3 finalPosition, initialPosition;
    void Start()
    {
        rb = extensionObject.GetComponent<Rigidbody>(); // Get the rigidbody component of the piston extender
        finalPosition = new Vector3(extensionObject.transform.position.x, extensionObject.transform.position.y + extensionHeight, extensionObject.transform.position.z);
        initialPosition = new Vector3(extensionObject.transform.position.x, extensionObject.transform.position.y, extensionObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)   // If the piston is moving
        {
            if (extending)  // If the piston is extending
            {
                rb.MovePosition(extensionObject.transform.position + (transform.up * pistonMoveSpeed * Time.deltaTime));  // Move upwards determening on the object's up vector
                if (extensionObject.transform.position.y >= finalPosition.y)    // If at / passed the max extension distance
                {
                    extensionObject.transform.position = finalPosition; // Set position to max extended position
                    isMoving = false;                                   // Disable moving
                }
            }
            else            // Piston is retracting
            {
                rb.MovePosition(extensionObject.transform.position + (transform.up * -pistonMoveSpeed * Time.deltaTime)); // Move downwards determening on the object's up vector
                if (extensionObject.transform.position.y <= initialPosition.y)
                {
                    extensionObject.transform.position = initialPosition;
                    isMoving = false;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if(!isMoving)
        {
            if (extending)
            {
                extensionObject.transform.position = finalPosition;
                rb.velocity = Vector3.zero;
            }
            else
            {
                extensionObject.transform.position = initialPosition;
                rb.velocity = Vector3.zero;
            }
        }
    }

    public void Extend()
    {
        isMoving = true;
        extending = true;
    }

    public void Retract()
    {
        isMoving = true;
        extending = false;
    }
}
