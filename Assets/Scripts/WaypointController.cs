using UnityEngine;
using System.Collections;

public class WaypointController : MonoBehaviour {

	private GameObject mainController;
	private BarberShop barberShopScript;
	void Start () {
		mainController = GameObject.Find("MainController");
		barberShopScript = GetComponentInParent<BarberShop>();
		if (!mainController) Debug.LogError("Object MainController not found!");
		if (!barberShopScript) Debug.LogError("BarberShop script not found in MainController");
	}

	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log(other.name + " entered in the " + this.name);
	}

	void OnTriggerStay2D (Collider2D other) {
		//Debug.Log(other.name + " is at " + this.name);
		if (!other.GetComponent<CustomerController>().waiting		// if not waiting AND not leaving...
			&& !other.GetComponent<CustomerController>().leaving)   // we are going to the reception
		{
			barberShopScript.handleCustomerInReception(other.gameObject);
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		//Debug.Log(other.name + " has left the " + this.name);
	}
}
