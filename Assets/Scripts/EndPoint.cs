using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour {

	public bool win;
	public GameObject Player;
	public GameObject EndMusic;
	public GameObject BGM;
	public GameObject WinWords;
	public GameObject Again;
//	public Canvas canv;

	// Use this for initialization
	void Start () {
		win = false;
		WinWords.SetActive (false);
		Again.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (win) {
			WinWords.SetActive (true);
			Again.SetActive (true);
		}
	}
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			win = true;
			BGM.GetComponent<AudioSource> ().Stop ();
			EndMusic.GetComponent<AudioSource> ().Play();
			Player.GetComponent<thirdPersonCamera> ().StopPeanut ();
		}
	}

	public void PlayAgain(){
		SceneManager.LoadScene ("main");
	}
}
