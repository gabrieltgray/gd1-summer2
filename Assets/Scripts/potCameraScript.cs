using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potCameraScript : MonoBehaviour {


    public Camera mainCamera;
    public GameObject player;
    thirdPersonCamera cameraScript;
	// Use this for initialization
	void Start () {
        cameraScript = player.GetComponent<thirdPersonCamera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void moveCamera() {
        //cameraScript.offset += new Vector3(10f, 10f, 0f);
    }

}
