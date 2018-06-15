using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotPanLaunch : MonoBehaviour {

    public float thrust;
    public float spinSpeed = 455.0f;      //rotation so that the pot slowly spins
    public float launchAngle = 45.0f;     //angle of launch
    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        transform.eulerAngles = new Vector3(launchAngle, launchAngle, 0);
        rb = GetComponent<Rigidbody>();
        Launch();
	}
	
	// Update is called once per frame
	void Update () {
        //I don't know why this works but this keeps the pots/pans from instantly rotating on play
        spinSpeed += Input.GetAxis("Vertical");
        transform.eulerAngles = new Vector3(0, spinSpeed, 0);
    }

    void Launch()
    {
        rb.AddForce(transform.eulerAngles * thrust);
    }
}
