using UnityEngine;
using System.Collections;

public class WaypointController : MonoBehaviour {

	private GameObject mainController;
	void Start () {
		mainController = GameObject.Find("MainController");
		if (!mainController) Debug.LogError("Object MainController not found!");
	}

	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log(other.name + " entered in the " + this.name);
	}

	void OnTriggerStay2D (Collider2D other) {
		Debug.Log(other.name + " is at " + this.name);
	}

	void OnTriggerExit2D (Collider2D other) {
		Debug.Log(other.name + " has left the " + this.name);
	}
}
