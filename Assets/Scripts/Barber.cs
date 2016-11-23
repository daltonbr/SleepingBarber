using UnityEngine;
using System.Collections;

public class Barber : MonoBehaviour {

	private bool Awake;

	void Start () {
		sleep();
	}

	public void sleep() {
		//TODO animate Barber sleeping
		this.Awake = false;
	}

	public void wakeUp() {
		//TODO animate Barber waking up
		this.Awake = true;
	}

	public bool isAwake() {
		return this.Awake;
	}
    
}
