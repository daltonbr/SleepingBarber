using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject customerPrefab;

    public int spawnTime = 3; // in seconds
	public int totalNumberOfCustomers = 3;

	void Start ()
	{
		for (int i = 0; i < totalNumberOfCustomers; i++) 
		{
			Invoke("spawnCustomer", spawnTime * i);  // invoke a method after some time
		}
    }

	void spawnCustomer()
	{
		Debug.Log("spawning a customer");
		Instantiate(customerPrefab, transform.position, Quaternion.identity);
	}
}
