using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

    //public list<BarberShop> barberFranchise;
    public Transform spawnPoint;
    public int spawnTimer;
    private BarberShop barbershop;
    
	void Start () {
        //reference to the barbershop script
        barbershop = this.GetComponent<BarberShop>();

	}
	
}
