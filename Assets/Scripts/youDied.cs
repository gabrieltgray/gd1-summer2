﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class youDied : MonoBehaviour {

    public float timeDelay;
    public GameObject redScreen;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponent<Transform>().position.y <= 11)
        {
            Instantiate(redScreen);
            Invoke("Death", timeDelay);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
		if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(redScreen);
            Invoke("Death", timeDelay);
        }
    }

    void Death()
    {
        SceneManager.LoadScene("main");
    }
}
