using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstMove : MonoBehaviour
{
    public Vector3 Direction;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 speed = Direction * Time.deltaTime;
        rb.MovePosition(transform.position + speed);
    }
}
