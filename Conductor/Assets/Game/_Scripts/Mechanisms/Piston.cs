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
        rb = extensionObject.GetComponent<Rigidbody>();
        finalPosition = new Vector3(extensionObject.transform.position.x, extensionObject.transform.position.y + extensionHeight, extensionObject.transform.position.z);
        initialPosition = new Vector3(extensionObject.transform.position.x, extensionObject.transform.position.y, extensionObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (extending)
            {
                rb.MovePosition(extensionObject.transform.position + (Vector3.up * pistonMoveSpeed * Time.deltaTime));
                if (extensionObject.transform.position.y >= finalPosition.y)
                {
                    extensionObject.transform.position = finalPosition;
                    isMoving = false;
                }
            }
            else
            {
                rb.MovePosition(extensionObject.transform.position + (Vector3.up * -pistonMoveSpeed * Time.deltaTime));
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
