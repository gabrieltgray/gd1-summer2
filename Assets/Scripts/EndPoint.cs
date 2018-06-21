using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {

	public bool win;
	public GameObject Player;
	public GameObject EndMusic;
	public GameObject BGM;

	// Use this for initialization
	void Start () {
		win = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			win = true;
			BGM.GetComponent<AudioSource> ().Stop ();
			EndMusic.GetComponent<AudioSource> ().Play();
			Player.GetComponent<thirdPersonCamera> ().StopPeanut ();
		}
	}
}
