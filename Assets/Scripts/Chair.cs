using UnityEngine;
using System.Collections;

public class Chair : MonoBehaviour {

	private bool occupied;
	public GameObject customer;

	// release a chair
	public void freeChair(GameObject chair)
	{
		occupied = false;
		customer = null;
	}

	// bind a customer to a chair
	public void occupyChair(GameObject _customer)
	{
		Debug.Log("occupying chair " + this.gameObject.name);
		occupied = true;
		customer = _customer;
	}

	public bool isOccupied()
	{
		return occupied;
	}

}
