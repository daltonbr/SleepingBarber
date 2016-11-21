using UnityEngine;
using System.Collections;

public class BarberShop : MonoBehaviour {

    public GameObject[] waitingChairs;
    public int waiting;
    public Chair barberChair;
    public Barber barber;
    public bool mutex;
    public GameObject customerTest;

    public void Start()
    {
        //Debug
        waitingChairs[0].GetComponent<Chair>().customer = customerTest;

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
            Debug.Log(chairScript.occupied);
            if (!chairScript.occupied) return chairScript.customer as GameObject;
        }
        return null;
    }

    public bool checkForEmptyChair(Customer customer)
    {
        return false;
    }

    public void sendToChair(GameObject customer, GameObject destinyChair)
    {
        Debug.Log("Customer was sent to chair");
        
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
    }

}
