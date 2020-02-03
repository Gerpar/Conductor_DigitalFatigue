using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        finalPosition = extensionObject.transform.position + extensionValue;
        initialPosition = extensionObject.transform.position;
        Debug.Log(gameObject.name + ": " + finalPosition + ", " + initialPosition);
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
            extensionObject.transform.position = position;
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
