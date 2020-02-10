using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robert Thomas
// Represents the height the water rests at

public class WaterLevel : MonoBehaviour
{
    public Water waterBody;                                                         // The body of water
    public float waterHeight { get { return gameObject.transform.position.y; } }    // The maximum height of the water
}
