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
	private BarberShop barberShopScript;
	//private Quaternion qTo;
	//public float speed = 85.0f;  // Degrees per second

	public void wakeUpBarber(Barber barber)
	{
		//TODO animate Customer going to the barber and waking up him
		//     or maybe just yelling at him
		barber.wakeUp();
	}
		
	void Start (){

		barberShopScript = GameObject.Find("MainController").GetComponent<BarberShop>();
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
			GameObject.Find("Barber").GetComponent<Barber>().wakeUp();
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
		
	public void sendTo (Transform destiny)
	{
		Vector3 moveDirection = destiny.position - this.transform.position;

		if (moveDirection.sqrMagnitude > 0.5f) {
			//qTo = Quaternion.LookRotation(moveDirection);
			
		}
//		float angle = Mathf.Atan2(moveDirection.y, destiny.position.x) * Mathf.Rad2Deg;
//		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
//		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
		//TODO: adjust the rotation dinamically to face the destiny
		//var rotation = Quaternion.LookRotation(moveDirection);
		//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * dampingLook);
		//transform.rotation = qTo;//Quaternion.RotateTowards(transform.rotation, qTo, speed * Time.fixedDeltaTime);

		//Vector3 vectorToTarget = targetTransform.position - transform.position;
		rb2D.AddForce(moveDirection.normalized * patrolVelocity * Time.fixedDeltaTime);
	}
}
