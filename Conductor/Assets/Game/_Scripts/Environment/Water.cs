using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//class to handle buoyancy of objects whilst in a "Water" tagged object
[RequireComponent(typeof(BoxCollider))]
public class Water : MonoBehaviour 
{
	public AudioClip splashSound;						//played when objects enter water
	public Vector3 force = new Vector3(0, 16.5f, 0);	//pushForce of the water. This is a vector3 so you can have force in any direction, for example a current or river
	public bool effectPlayerDrag;						//should the players rigidbody be effected by the drag/angular drag values of the water?
	public float resistance = 0.4f;						//the drag applied to rigidbodies in the water (but not player)
	public float angularResistance = 0.2f;				//the angular drag applied to rigidbodies in the water (but not player)
    public float speedLimit = 5.0f;

    private Dictionary<GameObject, float> dragStore = new Dictionary<GameObject, float>();
	private Dictionary<GameObject, float> angularStore = new Dictionary<GameObject, float>();
    private bool changingLevel = false;
	
	void Awake()
	{
        BoxCollider collide = GetComponent<BoxCollider>();

		if(tag != "Water")
		{
			tag = "Water";
			Debug.LogWarning("'Water' script attached to an object not tagged 'Water', it been assigned the tag 'Water'", transform);
		}

        collide.isTrigger = true;
        collide.center = Vector3.zero;
        collide.size = new Vector3(1, 1, 1);
	}
	
	//apply buoyancy
	void OnTriggerStay(Collider other)
	{
		if(other.tag != "Player" && other.TryGetComponent(out Rigidbody rigid))
		{
            float surface = transform.position.y + GetComponent<Collider>().bounds.extents.y; // get surface position
            float floor = transform.position.y - GetComponent<Collider>().bounds.extents.y;
            float depth = surface - other.transform.position.y; // get object depth
            Vector3 forceToAdd = Vector3.zero;

            if (other.TryGetComponent(out BaseMatter matter) && matter.IsBuoyant)
            {
                forceToAdd += Physics.gravity * rigid.mass * -1;
                
                if (rigid.velocity.y < 0)
                {
                    forceToAdd += force;
                }
                else if (other.transform.position.y < surface)
                {
                    forceToAdd += new Vector3(0, rigid.mass * (speedLimit - rigid.velocity.y), 0);
                }

                // forceToAdd += force * ((other.transform.position.y - surface) / (floor - surface));
            }
            else
            {
                forceToAdd += (Physics.gravity * rigid.mass * -1) * 0.9f;
            }

            rigid.AddForce(forceToAdd, ForceMode.Force);

            // //if below surface, push object
            // if (depth > 0.4f)
            //     forceToAdd += force;
            // //if we are near the surface, add less force, this prevents objects from "jittering" up and down on the surface
            // else
            //     forceToAdd += force * (depth * 2);
            // 
            // if (other.TryGetComponent(out BaseMatter matter) && matter.IsBuoyant)
            //     forceToAdd += Physics.gravity * -1;
            // 
            // rigid.AddForce(forceToAdd, ForceMode.Force);
        }
	}
	
	//sets drag on objects entering water
	void OnTriggerEnter(Collider other)
	{
		//rigidbody entered water?
		Rigidbody r = other.GetComponent<Rigidbody>();
		if(r)
		{
			if(splashSound)
			{
				float volume = other.GetComponent<Rigidbody>().velocity.magnitude/5;
				AudioSource.PlayClipAtPoint(splashSound, other.transform.position, volume);
			}
			//stop if we arent effecting player
			if (r.tag == "Player" && !effectPlayerDrag)
				return;

            //store objects default drag values (if it's values have not already been stored)
            if (!dragStore.ContainsKey(r.gameObject))
            {
                dragStore.Add(r.gameObject, r.drag);
                angularStore.Add(r.gameObject, r.angularDrag);
            }

			//apply new drag values to object
			r.drag = resistance;
			r.angularDrag = angularResistance;
		}
		else if(splashSound)
			AudioSource.PlayClipAtPoint(splashSound, other.transform.position);
	}
	
	//reset drag on objects leaving water
	void OnTriggerExit(Collider other)
	{
		//rigidbody entered water?
		Rigidbody r = other.GetComponent<Rigidbody>();
		if(r)
		{
			//stop if we arent effecting player
			if(r.tag == "Player" && !effectPlayerDrag)
				return;
			
			//see if we've stored this objects default drag values
			if (dragStore.ContainsKey(r.gameObject) && angularStore.ContainsKey(r.gameObject))
			{
				//restore values
				r.drag = dragStore[r.gameObject];
				r.angularDrag = angularStore[r.gameObject];
				//remove stored values for this object
				dragStore.Remove(r.gameObject);
				angularStore.Remove (r.gameObject);
			}
			else
			{
				//restore default values incase we cant find it in list (for whatever reason)
				r.drag = 0f;
				r.angularDrag = 0.05f;
				print ("Object left water: couldn't get drag values, restored to defaults");
			}
		}
	}

    // Raises or lowers the water level to the height of newWaterLevel
    public void ChangeWaterLevel(float newWaterLevel)
    {
        float waterMin = transform.position.y - gameObject.GetComponent<Renderer>().bounds.extents.y;

        transform.position = new Vector3(transform.position.x, (newWaterLevel + waterMin) / 2, transform.position.z);
        transform.localScale= new Vector3(transform.localScale.x, newWaterLevel - waterMin, transform.localScale.z);

        // Vector3 newPosition = transform.position;
        // 
        // // First moves the entire object to newWaterLevel, then lowers it so only the top of the water is positioned at newWaterLevel
        // newPosition.y += (newWaterLevel - transform.position.y) - gameObject.GetComponent<Renderer>().bounds.extents.y;
        // transform.position = newPosition;
    }
}