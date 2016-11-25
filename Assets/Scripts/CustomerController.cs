using UnityEngine;
using System.Collections;

public class CustomerController : MonoBehaviour {

	public Rigidbody2D rb2D;
	public bool waiting;
	public bool leaving;
	public bool cutting;
    public bool served;					 // control if the customer has their hair cut or not
	public Transform reception;
	public Transform exit;
	//public Transform chairToSit;
	public float patrolVelocity = 3f;    // The walking velocity between Waypoints
	public bool  loop = true;       	 // Do you want to keep repeating the Waypoints
	public float dampingLook= 6.0f;      // How slowly to turn
	public float pauseDuration = 0;      // How long to pause at a Waypoint
	private float curTime;
	private GameObject chairAssociated;	 // chair that the player will seat
	public GameObject deathParticle;
	public Sprite[] skins;
		
	void Start (){
		rb2D = GetComponent<Rigidbody2D>();
		reception = GameObject.Find("MainController").transform.FindChild("WaypointReception");
		exit = GameObject.Find("MainController").transform.FindChild("WaypointExit");
		if (!reception) Debug.LogError("Waypoint Reception not found as a child of MainController");
		if (!exit) Debug.LogError("WaypointExit not found as a child of MainController");
		this.GetComponent<SpriteRenderer>().sprite = skins[Random.Range(0,skins.Length)];
		deathParticle = GameObject.Find("DeathParticle");
	}

	void FixedUpdate () 
	{
		if (leaving)
		{
			sendTo(exit);
			return;
		}

		GameObject chair = this.getChairAssociated();
		//send to the chair associtaded with this customer
		if (chair)
		{
			sendTo(chair.transform);
			return;
		}

		// else...going to reception
		sendTo(reception);
	}
		
	public void sendTo (Transform destiny)
	{
		Vector3 moveDirection = destiny.position - this.transform.position;

		if (moveDirection.sqrMagnitude > 0.5f) {
			rb2D.AddForce(moveDirection.normalized * patrolVelocity * Time.fixedDeltaTime);
		}
	}

	// bind (set) a chair to the customer
	public void associateToChair(GameObject chair)
	{
		if (chair) {
			Chair chairScript = chair.GetComponent<Chair> ();
			chairScript.associated = true;
		}
		this.chairAssociated = chair;
	}

	// get the chair associated with the customer
	public GameObject getChairAssociated()
	{
		return this.chairAssociated;
	}
}
