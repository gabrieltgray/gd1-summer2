using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationControllerScript : MonoBehaviour {

    Animator anim;
    Rigidbody rb;
    thirdPersonCamera cameraScript;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cameraScript = GetComponent<thirdPersonCamera>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("playerSpeedX", cameraScript.frontMovement.magnitude);
        anim.SetFloat("playerSpeedY", transform.InverseTransformDirection(rb.velocity).x);
	}
}
