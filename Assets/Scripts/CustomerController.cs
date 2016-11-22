using UnityEngine;
using System.Collections;

public class CustomerController : MonoBehaviour {

	public Rigidbody2D rb2D;
    public bool served;					 // control if the customer has their hair cut or not
	//public Transform[] waypoints;         // The amount of Waypoint you want
	public Transform reception;
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

    public void leave()
    {

    }

	void Start (){

		customerController = GetComponent<CustomerController>();
		rb2D = GetComponent<Rigidbody2D>();
		reception = GameObject.Find("MainController").transform.FindChild("WaypointReception");
		if (!reception) Debug.LogError("Waypoint Reception not found as a child of MainController");
		//Debug.Log(reception.name + " is at " + reception.transform);
	}

	void FixedUpdate () 
	{

		//if(currentWaypoint < waypoints.Length){
			patrol();
		//}else{    
		//	if(loop){
		//		currentWaypoint=0;
		//	} 
		//}
	}

	void patrol ()
	{

		//Vector3 target = this.waypoints[currentWaypoint].position;
		//target.y = transform.position.y; // Keep waypoint at customer's height
		Vector3 moveDirection = reception.transform.position - this.transform.position;

		if(moveDirection.magnitude < 1f){
			if (curTime == 0)
				curTime = Time.time; // Pause over the Waypoint
			if ((Time.time - curTime) >= pauseDuration){
				//Debug.Log("waypoint++");
				//currentWaypoint++;
				curTime = 0;
			}
		}else{        
			//var rotation = Quaternion.LookRotation(target - transform.position);
			//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * dampingLook);

			rb2D.AddForce(moveDirection.normalized * patrolVelocity * Time.fixedDeltaTime/* * moveDirection.magnitude*/);
			//Debug.Log(moveDirection.normalized * patrolVelocity * Time.fixedDeltaTime + "" + Time.fixedDeltaTime);
		}  
	}

}
