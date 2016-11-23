using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarberShop : MonoBehaviour {

    public GameObject[] waitingChairs;

    public int waitingCount;
	public int customersTotalCount;
	public CustomerController customerController;
    public GameObject barberChair;
    public GameObject barber;
	public Barber barberScript;
    private bool mutex;  // false: unlocked, true: locked
	public GameObject waypointReception;
	public Transform waypointExit;
	public Text textCustomersValue;
	public Text textWaitingValue;
	public Text textChairsValue;
	public Toggle toggleMutex;		// reference to ToggleMutex Script

    public void Start()
    {
		barber = GameObject.Find("Barber");
		if(!barber) Debug.LogError("Barber GameObject not found!");
		barberScript = barber.GetComponent<Barber>();
		textCustomersValue  = GameObject.Find("TextCustomersValue").GetComponent<Text>();
		textWaitingValue  = GameObject.Find("TextWaitingValue").GetComponent<Text>();
		textChairsValue  = GameObject.Find("TextChairsValue").GetComponent<Text>();
		toggleMutex = GameObject.Find("ToggleMutex").GetComponent<Toggle>();

		barberWorking();		// barber's loop
    }

    public GameObject getNextCustomer()
    {
        foreach (GameObject chair in waitingChairs)
        {
            Chair chairScript = chair.GetComponent<Chair>();
			if (chairScript.isOccupied()) return chairScript.customer.gameObject;
        }
        return null;
    }

	public void handleCustomerInReception(GameObject customer)
	{
		GameObject chair;
		if (!isLocked()) 
		{
			lockMutex();
			// wakeup barber anyway
			barberScript.wakeUp();

			// if we have a free chair
			if (chair = this.checkForEmptyChair())  
			{
				Debug.Log(customer.name + " will wait in: " + chair.name);
				waitingCount++;
				textWaitingValue.text = waitingCount.ToString();
				customer.GetComponent<CustomerController>().waiting = true;
				sendToChair(customer, chair);
				unlockMutex();
			}
			else // dont have a free chair, customer leaving
			{
				Debug.Log("All chairs occupied. Customer leaving!");
				customer.GetComponent<CustomerController>().leaving = true;
				customersTotalCount--;
				sendTo(customer,waypointExit);
				unlockMutex();
			}
		}
	}

    public GameObject checkForEmptyChair()
    {
		foreach(GameObject chair in waitingChairs)
		{
			Chair chairScript = chair.GetComponent<Chair>();
			//Debug.Log(chairScript.occupied);
			if (!chairScript.isOccupied())  // if we have free chairs, return it
			{
				Debug.Log(chairScript.gameObject.name + "is empty");
				return chairScript.gameObject;
			}
		}
        return null;
    }

    public void sendToChair(GameObject customer, GameObject destinyChair)
    {
        Debug.Log("Customer was sent to " + destinyChair.name);
		destinyChair.GetComponent<Chair>().occupyChair(customer);	//bind customer to a chair
		customer.GetComponent<CustomerController>().associateToChair(destinyChair);  // bind chair to customer
		unlockMutex();
    }

	public void sendTo(GameObject customer, Transform destiny)
	{
		Debug.Log("Customer was sent to " + destiny.name);
		this.textCustomersValue.text = this.customersTotalCount.ToString();
		unlockMutex();
	}

	public void makeBarberCutHair()
	{
		
	}

    public void sendToBarberChair(GameObject customer)
    {

		// get the chair that the customer is seated
		GameObject chairAssociated = customer.GetComponent<CustomerController>().getChairAssociated();

		//free that chair
		chairAssociated.GetComponent<Chair>().freeChair(chairAssociated);

		// logically binds the customer to the barberChair
		customer.GetComponent<CustomerController>().associateToChair(barberChair);

		// move visually the customer to barberChair
		sendTo(customer, barberChair.transform);
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

	public void lockMutex()
	{
		mutex = true;
		toggleMutex.isOn = mutex;
	}

	public void unlockMutex()
	{
		mutex = false;
		toggleMutex.isOn = mutex;
	}

	public bool isLocked()
	{
		return mutex;
	}
}
