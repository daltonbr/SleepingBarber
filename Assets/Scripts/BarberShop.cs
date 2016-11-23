using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarberShop : MonoBehaviour {

    public GameObject[] waitingChairs;

    public int waitingCount;
	public int customersTotalCount;
	public CustomerController customerController;
    public Chair barberChair;
    public GameObject barber;
	public Barber barberScript;
    public bool mutex;  // false: unlocked, true: locked
	public GameObject waypointReception;
	public Transform waypointExit;
	public Text textCustomersValue;
	public Text textWaitingValue;
	public Text textChairsValue;

    public void Start()
    {
		barber = GameObject.Find("Barber");
		if(!barber) Debug.LogError("Barber GameObject not found!");
		barberScript = barber.GetComponent<Barber>();
		textCustomersValue  = GameObject.Find("TextCustomersValue").GetComponent<Text>();
		textWaitingValue  = GameObject.Find("TextWaitingValue").GetComponent<Text>();
		textChairsValue  = GameObject.Find("TextChairsValue").GetComponent<Text>();

		barberWorking();
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
			if (!barberScript.isAwake())
				customer.GetComponent<CustomerController>().wakeUpBarber(barberScript);

			if (chair = this.checkForEmptyChair(customer))  // we have a free chair
			{
				Debug.Log("Find an empty chair:");
				waitingCount++;   // move this variable to MainController if we have time
				textWaitingValue.text = waitingCount.ToString();
				sendToChair(customer, chair);
			}
			else // dont have a free chair, customer leaving
			{
				mutex = false;
				Debug.Log("All chairs occupied. Customer leaving!");
				customer.GetComponent<CustomerController>().leaving = true;
				customersTotalCount--;
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
				Debug.Log(chairScript.gameObject.name + "is empty");
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
		mutex = false;  // releasing thes mutex
    }

	public void sendTo(GameObject customer, Transform destiny)
	{
		Debug.Log("Customer was sent to " + destiny.name);
		this.textCustomersValue.text = this.customersTotalCount.ToString();
		mutex = false;  // releasing the mutex
	}

	public void makeBarberCutHair()
	{

	}

    public void sendToBarberChair(GameObject customer)
    {

    }

    public void cutHair(GameObject customer)
    {
		sendToBarberChair(customer);
		makeBarberCutHair();
    }

    public void barberWorking()
    {
		GameObject customerToCutHair;
		while (customerToCutHair = getNextCustomer())
        {
			cutHair(customerToCutHair);
        }
		Debug.Log("There is no more customers");
		barberScript.sleep();
    }

	public void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log(other.name);	
	}

	//TODO: this is optional...not really needed if we set the number of waiting chairs manually
	public int countChairsOnTheScene()  // minus the BarberChair
	{
		//TODO: loop through the scene counting how many chairs we have,
		// then we subtract the barberChair
		// this method could be used to populate the UI number of waitingChairs
		return 0;
	}

}
