using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script created by Gerad paris
/// <summary>
/// Controls the piston's extension, and how far it will extend outwards, and in which direction it will move.
/// </summary>
public class Piston : MonoBehaviour
{
    [SerializeField] float pistonMoveSpeed;
    [SerializeField] Vector3 extensionValue;
    [SerializeField] GameObject extensionObject;

    private bool extending = false;
    private bool isMoving = false;

    private Vector3 finalPosition, initialPosition;
    void Start()
    {
        finalPosition = extensionObject.transform.position + extensionValue;    // Create a final extension position (Where the piston head will be when fully extended)
        initialPosition = extensionObject.transform.position;                   // Initial position of the piston head (Fully retracted)
    }

    IEnumerator MoveToPosition(Vector3 initialPos, Vector3 newPos, float movespeed)
    {
        isMoving = true;
        while(Vector3.Distance(extensionObject.transform.position, newPos) > 0.05f)
        {
            extensionObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(extensionObject.transform.position, newPos, pistonMoveSpeed * Time.deltaTime)); // Move towards the end point over time
            yield return null;
        }

        StartCoroutine(StayAtPosition(newPos)); // Locks the position to the new position
    }

    IEnumerator StayAtPosition(Vector3 position)
    {
        isMoving = false;
        while(true)
        {
            extensionObject.transform.position = position;
            yield return null;
        }
    }

    public void Extend()
    {
        StopAllCoroutines();
        if(!isMoving)
        {
            StartCoroutine(MoveToPosition(initialPosition, finalPosition, pistonMoveSpeed));
        }
    }

    public void Retract()
    {
        StopAllCoroutines();
        if(!isMoving)
        {
            StartCoroutine(MoveToPosition(finalPosition, initialPosition, pistonMoveSpeed));
        }
    }
}
