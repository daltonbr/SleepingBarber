using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

    public Transform spawnPoint;
    public int spawnTimer;
   
	void Start () {
        
	}
	public void OnGUI() {
		//just to show how to make a button on GUI - this is hardcoded, not dynamical
		if (GUI.Button(new Rect(10, 10, 150, 100), "Pause"))
		{
			print("pause/unpause");
			if (Time.timeScale == 0) Time.timeScale = 1;   // pause toggle / 1 = 100%, 0 = 0%
				else Time.timeScale = 0;
		}
	}
		
}
