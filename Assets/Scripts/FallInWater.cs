using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallInWater : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void FixedUpdate(){
		if (gameObject.GetComponent<Transform> ().position.y <= 11) 
		{
			SceneManager.LoadScene("main");
			print("reset");
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
