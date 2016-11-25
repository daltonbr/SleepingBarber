using UnityEngine;
using System.Collections;

public class WaypointController : MonoBehaviour {

	private GameObject mainController;
	private BarberShop barberShopScript;
	private Barber barberScript;
	void Start () {
		barberScript = GameObject.Find("Barber").GetComponent<Barber>();
		mainController = GameObject.Find("MainController");
		barberShopScript = GetComponentInParent<BarberShop>();
		if (!mainController) Debug.LogError("Object MainController not found!");
		if (!barberShopScript) Debug.LogError("BarberShop script not found in MainController");
		if (!barberScript) Debug.LogError("Barber script not found");
	}

	void OnTriggerEnter2D (Collider2D other) {
		//Debug.Log(other.name + " entered in the " + this.name);
	}

	void OnTriggerStay2D (Collider2D other) {
		if (!other.GetComponent<CustomerController>().waiting		// if not waiting AND
			&& !other.GetComponent<CustomerController>().leaving    // not leaving AND
			&& !other.GetComponent<CustomerController>().cutting)   // not cutting - we are going to the reception
		{
			Debug.Log("handleCustomer");
			barberShopScript.handleCustomerInReception(other.gameObject);
		}

		//Debug.Log(other.name + " is at " + this.name);
	}

	void OnTriggerExit2D (Collider2D other) {
		//Debug.Log(other.name + " has left the " + this.name);
	}
}
