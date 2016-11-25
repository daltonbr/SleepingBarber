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
	//private BarberShop barberShopScript;
	private GameObject chairAssociated;	 // chair that the player will seat

//	public void wakeUpBarber(Barber barber)
//	{
//		//TODO animate Customer going to the barber and waking up him
//		//     or maybe just yelling at him
//		barber.wakeUp();
//	}
		
	void Start (){

		//barberShopScript = GameObject.Find("MainController").GetComponent<BarberShop>();
		rb2D = GetComponent<Rigidbody2D>();
		reception = GameObject.Find("MainController").transform.FindChild("WaypointReception");
		exit = GameObject.Find("MainController").transform.FindChild("WaypointExit");
		if (!reception) Debug.LogError("Waypoint Reception not found as a child of MainController");
		if (!exit) Debug.LogError("WaypointExit not found as a child of MainController");
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
		this.chairAssociated = chair;
	}

	// get the chair associated with the customer
	public GameObject getChairAssociated()
	{

		return this.chairAssociated;
	}
}
