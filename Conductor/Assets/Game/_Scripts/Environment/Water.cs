using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//class to handle buoyancy of objects whilst in a "Water" tagged object
[RequireComponent(typeof(BoxCollider))]
public class Water : MonoBehaviour 
{
	public AudioClip splashSound;						// Played when objects enter water
	public Vector3 force = new Vector3(0, 16.5f, 0);	// PushForce of the water. This is a vector3 so you can have force in any direction, for example a current or river
	public bool effectPlayerDrag;						// Should the players rigidbody be effected by the drag/angular drag values of the water?
	public float resistance = 0.4f;						// The drag applied to rigidbodies in the water (but not player)
	public float angularResistance = 0.2f;				// The angular drag applied to rigidbodies in the water (but not player)
    public float floatSpeed = 5.0f;                     // The maximum speed objects can move when being buoyed upward in this body of water
    public float waterSpeed = 1.0f;                     // The speed the water travels when it is changing level

    private Dictionary<GameObject, float> dragStore = new Dictionary<GameObject, float>();
	private Dictionary<GameObject, float> angularStore = new Dictionary<GameObject, float>();
	
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
	
	// Apply buoyancy
	void OnTriggerStay(Collider other)
	{
		if(other.tag != "Player" && other.TryGetComponent(out Rigidbody rigid))
		{
            float surface = transform.position.y + GetComponent<Collider>().bounds.extents.y;   // Position of the water's surface
            Vector3 forceToAdd = Vector3.zero;                                                  // How much force will be applied to the object

            // If the object is buoyant, then it will be pushed upward until it reaches the water's surface
            if (other.TryGetComponent(out BaseMatter matter) && matter.IsBuoyant)
            {
                forceToAdd += Physics.gravity * rigid.mass * -1;    // Counterforce to gravity
                
                // If object is moving downward, the full push force is applied against it
                if (rigid.velocity.y < 0)
                {
                    forceToAdd += force;
                }
                // Once the falling object has come to a stop, force is applied until it reaches the surface
                else if (other.transform.position.y < surface)
                {
                    forceToAdd += new Vector3(0, rigid.mass * (floatSpeed - rigid.velocity.y), 0); // Gradually accelerates the object until it reaches floatSpeed
                }
            }
            // If the object is not buoyant, then it's descent is slowed
            else
            {
                forceToAdd += (Physics.gravity * rigid.mass * -1) * 0.9f; // Slows the objects descent to 10% of the force it should be experiencing
            }

            rigid.AddForce(forceToAdd, ForceMode.Force);
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

    // Animates the water rising or lowering to the height of newWaterLevel
    public IEnumerator ChangeWaterLevel(float newWaterLevel)
    {
        Renderer waterRenderer = gameObject.GetComponent<Renderer>();

        float currentWaterLevel = transform.position.y + waterRenderer.bounds.extents.y;    // The current position of the water's surface
        float waterMin = transform.position.y - waterRenderer.bounds.extents.y;             // The position of the sea floor

        // Lower the water level
        if (currentWaterLevel > newWaterLevel)
        {
            // Lowers the water level each frame until it has almost surpassed newWaterLevel
            while(!((currentWaterLevel - (waterSpeed * Time.deltaTime)) <= newWaterLevel))
            {
                currentWaterLevel -= (waterSpeed * Time.deltaTime);                                                                 // Lower the water level by a factor of deltaTime
                transform.position = new Vector3(transform.position.x, (currentWaterLevel + waterMin) / 2, transform.position.z);   // Repositions the body of water in preparation to be rescaled
                transform.localScale = new Vector3(transform.localScale.x, currentWaterLevel - waterMin, transform.localScale.z);   // Scales the body of water so that the top of the water
                                                                                                                                    // reaches currentWaterLevel and the bottom reaches waterMin

                yield return null;
            }

            // For the final frame, the body of water is repositioned and rescaled so it matches newWaterLevel exactly
            transform.position = new Vector3(transform.position.x, (newWaterLevel + waterMin) / 2, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x, newWaterLevel - waterMin, transform.localScale.z);
        }
        // Raise the water level
        else if (currentWaterLevel < newWaterLevel)
        {
            // Raises the water level each frame until it has almost surpassed newWaterLevel
            while (!((currentWaterLevel + (waterSpeed * Time.deltaTime)) >= newWaterLevel))
            {
                currentWaterLevel += (waterSpeed * Time.deltaTime);                                                                 // Raise the water level by a factor of deltaTime
                transform.position = new Vector3(transform.position.x, (currentWaterLevel + waterMin) / 2, transform.position.z);   // Repositions the body of water in preparation to be rescaled
                transform.localScale = new Vector3(transform.localScale.x, currentWaterLevel - waterMin, transform.localScale.z);   // Scales the body of water so that the top of the water
                                                                                                                                    // reaches currentWaterLevel and the bottom reaches waterMin

                yield return null;
            }

            // For the final frame, the body of water is repositioned and rescaled so it matches newWaterLevel exactly
            transform.position = new Vector3(transform.position.x, (newWaterLevel + waterMin) / 2, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x, newWaterLevel - waterMin, transform.localScale.z);
        }
        // If the water is already at newWaterLevel, then nothing is done
    }
}