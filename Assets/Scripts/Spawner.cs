using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public GameObject customerPrefab;

    public int minSpawnTime = 2; // in seconds
	public int maxSpawnTime = 5; // in seconds
	public int totalNumberOfCustomers = 3;
	public Text textCustomersValue;
	public BarberShop barberShopScript;

	void Start ()
	{
		barberShopScript = GameObject.Find("MainController").GetComponent<BarberShop>();
		textCustomersValue  = GameObject.Find("TextCustomersValue").GetComponent<Text>();

		for (int i = 0; i < totalNumberOfCustomers; i++) 
		{
			Invoke("spawnCustomer", Random.Range(minSpawnTime, maxSpawnTime) * i);  // invoke a method after some random time
		}
    }

	void spawnCustomer()
	{
		barberShopScript.customersTotalCount++;
		textCustomersValue.text = barberShopScript.customersTotalCount.ToString();

//		Debug.Log("spawning a customer");
		Instantiate(customerPrefab, transform.position, Quaternion.identity);
	}
}
