using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Barber : MonoBehaviour {
	//public bool cutting;
	public Barber barberScript; 	
	public Toggle toggleAwake;		// reference to ToggleAwake Script
	public BarberShop barbershopScript;
	public Chair barberChairScript;
	private bool awake;

	void Start () {
		barbershopScript = GameObject.Find("MainController").GetComponent<BarberShop>();
		barberChairScript = GameObject.Find ("barberChair").GetComponent<Chair>();
		barberScript = this.GetComponent<Barber>();
		toggleAwake = GameObject.Find("ToggleAwake").GetComponent<Toggle>();
		sleep();
	}

	public void sleep() {
		//TODO animate Barber sleeping
		Debug.Log("Barber Sleeps ZzZ");
		this.awake = false;
		toggleAwake.isOn = this.awake;
	}

	public void wakeUp() {
		//TODO animate Barber waking up
		Debug.Log("Barber is Waking Up");
		this.awake = true;
		toggleAwake.isOn = this.awake;
		//barbershopScript.barberWorking();		// barber's loop
		barberLoop();
	}

	public void barberLoop ()  //or FixedUpdate?
	{
		Debug.Log("barber's loop");
		GameObject customerToCutHair;

		if (customerToCutHair = barbershopScript.getNextCustomer())  // return null only if there isnt any more customers
		{
			//Debug.Log("Barber aquired a costumer");
			if (!barberChairScript.isOccupied())
				barbershopScript.aquireCustomer(customerToCutHair);
		}

		if (!customerToCutHair) {
			Debug.Log ("There is no more customers");
			barberScript.sleep ();
		}
	}

	public bool isAwake() {
		return this.awake;
	}

	public void OnGUI()
	{
		//toggleAwake.isOn = this.Awake;
	}

}
