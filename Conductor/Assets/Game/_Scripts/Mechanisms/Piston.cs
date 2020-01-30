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

    private Vector3 finalPosition, initialPosition;
    void Start()
    {
        finalPosition = new Vector3(extensionObject.transform.position.x, extensionObject.transform.position.y + extensionHeight, extensionObject.transform.position.z);
        initialPosition = new Vector3(extensionObject.transform.position.x, extensionObject.transform.position.y, extensionObject.transform.position.z);
    }

    IEnumerator MoveToPosition(Vector3 initialPos, Vector3 newPos, float movespeed)
    {
        while(Vector3.Distance(extensionObject.transform.position, finalPosition) > 0.05f)
        {
            extensionObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(extensionObject.transform.position, newPos, pistonMoveSpeed * Time.deltaTime)); // Move towards the end point over time
            yield return null;
        }

        StartCoroutine(StayAtPosition(newPos)); // Locks the position to the new position
    }

    IEnumerator StayAtPosition(Vector3 position)
    {
        while(true)
        {
            extensionObject.GetComponent<Rigidbody>().position = position;
            yield return null;
        }
    }

    public void Extend()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToPosition(initialPosition, finalPosition, pistonMoveSpeed));
    }

    public void Retract()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToPosition(finalPosition, initialPosition, pistonMoveSpeed));
    }
}
