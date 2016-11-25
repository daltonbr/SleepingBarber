using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarberShop : MonoBehaviour {

    public GameObject[] waitingChairs;

	public float cutHairDuration = 5f;
	public float timerToChair = 0f;  // do we really need a timer here?
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
    }

    public GameObject getNextCustomer()
    {
		//if (!isLocked())
		//{
		//	lockMutex();
			Debug.Log("getting next customer");
	        foreach (GameObject chair in waitingChairs)
	        {
	            Chair chairScript = chair.GetComponent<Chair>();
				if (chairScript.isOccupied()) 
				{
		//			unlockMutex();
					return chairScript.customer.gameObject;
	        	}
			}
		//	unlockMutex();
			Debug.Log("cant get next customer");
		//}
	    return null;
    }

	public void handleCustomerInReception(GameObject customer)
	{
		if (!isLocked()) 
		{
			GameObject chair;
			lockMutex();

			// if we have a free chair
			if (chair = this.checkForEmptyChair())  
			{
//				Debug.Log(customer.name + " will wait in: " + chair.name);
				waitingCount++;
				textWaitingValue.text = waitingCount.ToString();
				customer.GetComponent<CustomerController>().waiting = true;
				//this need to be a coroutine, because we need to set a timer
				
				sendToChair(customer, chair);
			}
			else // dont have a free chair, customer leaving
			{
				Debug.Log("All chairs occupied. Customer leaving!");
				customer.GetComponent<CustomerController>().leaving = true;
				customersTotalCount--;
				this.textCustomersValue.text = this.customersTotalCount.ToString();
			}
			unlockMutex();
		}
	}

    public GameObject checkForEmptyChair()
    {
		foreach(GameObject chair in waitingChairs)
		{
			Chair chairScript = chair.GetComponent<Chair>();
			//Debug.Log(chairScript.occupied);
			if (!chairScript.associated && !chairScript.isOccupied())  // if we have free chairs, return it
			{
				//Debug.Log(chairScript.gameObject.name + "is empty");
				return chairScript.gameObject;
			}
		}
        return null;
    }

    void sendToChair(GameObject customer, GameObject destinyChair)
    {
        Debug.Log("Customer was sent to " + destinyChair.name);
		//destinyChair.GetComponent<Chair>().occupyChair(customer);	//bind customer to a chair
		customer.GetComponent<CustomerController>().associateToChair(destinyChair);  // bind chair to customer
    }

	// IEnumerator is needed in order to make a time pause - this is a coroutine
	IEnumerator makeBarberCutHairCoroutine() 
	{
		// references
		Chair barberChairScript = barberChair.GetComponent<Chair>();
		GameObject customerCuttingHair = barberChairScript.customer.gameObject;
		
		Debug.Log("barber is cutting some hair =)");
		customerCuttingHair.GetComponent<CustomerController>().served = true;
		yield return new WaitForSeconds(cutHairDuration);   // a little pause
		//TODO: some animation would be nice though

		// freeing the barber chair
		barberChairScript.freeChair();

		customerCuttingHair.GetComponent<CustomerController>().leaving = true;
		Debug.Log("Hair cutted");
	}

	//TODO: turn this to IEnumerator? (to pause)
    public void sendToBarberChair(GameObject customer)
    {
		waitingCount--;
		Debug.Log("Sending to barber Chair");
		// get the chair that the customer is seated
		GameObject chairAssociated = customer.GetComponent<CustomerController>().getChairAssociated();

		//free that chair (if any)
		if (chairAssociated) chairAssociated.GetComponent<Chair>().freeChair();

		// logically binds the customer to the barberChair
		customer.GetComponent<CustomerController>().associateToChair(barberChair);
		customer.GetComponent<CustomerController>().waiting = false;	// he is no more waiting in the reception
		customer.GetComponent<CustomerController>().cutting = true;	// he is no more waiting in the reception

		// bind the chair to the customer (this is not ideal, we have cross-references here)
		barberChair.GetComponent<Chair>().occupyChair(customer);
		Debug.Log ("Barber Chair is being occupied");
    }

    public void aquireCustomer(GameObject customer)
    {
		Debug.Log("barber is about to call the customer"); 
		if (!isLocked())
		{
			lockMutex();
			sendToBarberChair(customer);
			StartCoroutine(makeBarberCutHairCoroutine());
			unlockMutex();
		}
		// goes to standby
			
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
		Debug.Log(" ----  lockMutex");
		mutex = true;
		toggleMutex.isOn = mutex;
	}

	public void unlockMutex()
	{
		Debug.Log(" ----  (UN)lockMutex");
		mutex = false;
		toggleMutex.isOn = mutex;
		//this.SendMessage("mutexAvailableBroadcast");
	}

	public bool isLocked()
	{
		return mutex;
	}
}
