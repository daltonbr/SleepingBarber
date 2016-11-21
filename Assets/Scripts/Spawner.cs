using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject customerPrefab;

    public int spawnTime = 3000; // in ms
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 4; i++)
            Instantiate(customerPrefab, transform.position, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
