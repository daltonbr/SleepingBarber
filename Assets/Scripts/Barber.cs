using UnityEngine;
using System.Collections;

public class Barber : MonoBehaviour {

	private bool Awake;

	void Start () {
		sleep();
	}

	public void sleep() {
		//TODO animate Barber sleeping
		Debug.Log("Barber Sleeps ZzZ");
		this.Awake = false;
	}

	public void wakeUp() {
		//TODO animate Barber waking up
		Debug.Log("Barber is Waking Up");
		this.Awake = true;
	}

	public bool isAwake() {
		return this.Awake;
	}    
}
