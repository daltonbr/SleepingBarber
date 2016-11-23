using UnityEngine;
using System.Collections;

public class CustomerController : MonoBehaviour {

	public Rigidbody2D rb2D;
	public bool waiting;
	public bool leaving;
    public bool served;					 // control if the customer has their hair cut or not
	//public Transform[] waypoints;         // The amount of Waypoint you want
	public Transform reception;
	public Transform exit;
	public Transform chairToSit;
	public float patrolVelocity = 3f;    // The walking velocity between Waypoints
	public bool  loop = true;       	 // Do you want to keep repeating the Waypoints
	public float dampingLook= 6.0f;      // How slowly to turn
	public float pauseDuration = 0;      // How long to pause at a Waypoint
	private float curTime;
	private int currentWaypoint = 0;
	private CustomerController customerController; // reference to the customer' script

    public void checkForChairs()
    {

    }

    public void leave(Transform destiny)
    {
		this.leaving = true;
		Vector3 moveDirection = destiny.position - this.transform.position;
		rb2D.AddForce(moveDirection.normalized * patrolVelocity * Time.fixedDeltaTime/* * moveDirection.magnitude*/);
    }

	void Start (){

		customerController = GetComponent<CustomerController>();
		rb2D = GetComponent<Rigidbody2D>();
		reception = GameObject.Find("MainController").transform.FindChild("WaypointReception");
		exit = GameObject.Find("MainController").transform.FindChild("WaypointExit");
		if (!reception) Debug.LogError("Waypoint Reception not found as a child of MainController");
		if (!exit) Debug.LogError("WaypointExit not found as a child of MainController");
		//Debug.Log(reception.name + " is at " + reception.transform);
	}

	void FixedUpdate () 
	{
		if (waiting) {
			//send to appriated chair and stop moving for a while
			sendTo(chairToSit);
			return; }
		if (leaving)
		{
			sendTo(exit);
		}
		else //going to reception
		{
			sendTo(reception);
		}
	}
		
	void sendTo (Transform destiny)
	{
		Vector3 moveDirection = destiny.position - this.transform.position;

		//TODO: adjust the rotation dinamically to face the destiny
		//var rotation = Quaternion.LookRotation(target - transform.position);
		//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * dampingLook)

		rb2D.AddForce(moveDirection.normalized * patrolVelocity * Time.fixedDeltaTime);
	}
}
