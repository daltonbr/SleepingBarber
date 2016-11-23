using UnityEngine;
using System.Collections;

public class BarberShop : MonoBehaviour {

    public GameObject[] waitingChairs;
    public int waiting;
    public Chair barberChair;
    public Barber barber;
    public bool mutex;  // false: unlocked, true: locked
    public GameObject customerTest;
	public GameObject waypointReception;
	public Transform waypointExit;

    public void Start()
    {
        //Debug
   //     waitingChairs[0].GetComponent<Chair>().customer = customerTest;

        barberWorking();
    }

    public void makeBarberCutHair()
    {

    }

    public GameObject getNextCustomer()
    {
        foreach (GameObject chair in waitingChairs)
        {
            Chair chairScript = chair.GetComponent<Chair>();
            //Debug.Log(chairScript.occupied);
            if (chairScript.occupied) return chairScript.customer as GameObject;
        }
        return null;
    }

	public void handleCustomerInReception(GameObject customer)
	{
		GameObject chair;
		Debug.Log(customer.name + " will be handled by the Barber!");
		if (!mutex) 
		{
			mutex = true; Debug.Log("mutex true");
			if (!this.barber.isAwake)
				customer.GetComponent<CustomerController>().wakeUpBarber(barber);

			if (chair = this.checkForEmptyChair(customer))  // we have a free chair
			{
				Debug.Log("Find an empty chair:");
				sendToChair(customer, chair);
			}
			else // dont have a free chair, customer leaving
			{
				mutex = false;
				Debug.Log("All chairs occupied. Customer leaving!");
				sendTo(customer,waypointExit);
			}
				
		}
	}

    public GameObject checkForEmptyChair(GameObject customer)
    {
		foreach(GameObject chair in waitingChairs)
		{
			Chair chairScript = chair.GetComponent<Chair>();
			//Debug.Log(chairScript.occupied);
			if (!chairScript.occupied)  // if we have free chairs, return it
			{
				Debug.Log("chair " + chairScript.gameObject.name + "is empty");
				return chairScript.gameObject;
			}
		}
        return null;
    }

    public void sendToChair(GameObject customer, GameObject destinyChair)
    {
        Debug.Log("Customer was sent to chair" + destinyChair.name);
		destinyChair.GetComponent<Chair>().customer = customer;
		destinyChair.GetComponent<Chair>().occupied = true;
		customer.GetComponent<CustomerController>().waiting = true;
		customer.GetComponent<CustomerController>().chairToSit = destinyChair.transform;
		mutex = false;  // releasing the mutex
    }

	public void sendTo(GameObject customer, Transform destiny)
	{
		Debug.Log("Customer was sent to " + destiny.name);
		//destinyChair.GetComponent<Chair>().customer = customer;
		//destinyChair.GetComponent<Chair>().occupied = true;
		customer.GetComponent<CustomerController>().leave(destiny);
		//mutex = false;  // releasing the mutex

	}

    public void sendToBarberChair(GameObject customer)
    {

    }

    public void cutHair()
    {

    }

    public void barberWorking()
    {
        while (getNextCustomer())
        {
			
        }
		//barber.GetComponent<Barber>().isAwake = true;
    }

	public void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.name);	
	}
}
