using UnityEngine;
using System.Collections;

public class Chair : MonoBehaviour {

	private bool occupied;
	public bool associated;
	public GameObject customer;
	private Barber barberScript;

	// release a chair
	public void freeChair()
	{
		associated = false;
		occupied = false;
		if(customer)
			customer.GetComponent<CustomerController>().associateToChair(null);
		customer = null;
	}

	// bind a customer to a chair
	public void occupyChair(GameObject _customer)
	{
		//Debug.Log("occupying chair " + this.gameObject.name);
		occupied = true;
		customer = _customer;
	}

	public bool isOccupied()
	{
		return occupied;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (!this.name.Equals ("barberChair")) {
			Debug.Log ("Char Triggered: " + other.name + " entered in the " + this.name);
			GameObject customer = (GameObject)other.gameObject;
			this.occupyChair (customer);

			Barber barberScript = GameObject.Find ("Barber").GetComponent<Barber> ();

			// awake the barber if he is sleeping
			if (barberScript) {
				if (!barberScript.isAwake ())
					barberScript.wakeUp ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (this.name.Equals ("barberChair")) {
			this.occupied = false;
			Debug.Log ("Barber Chair is not occupied anymore");

			Barber barberScript = GameObject.Find ("Barber").GetComponent<Barber> ();
			barberScript.barberLoop ();
		}
	}
}
