using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public bool playerDetected = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }
}
