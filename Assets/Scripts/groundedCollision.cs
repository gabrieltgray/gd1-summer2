using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundedCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision)
	{
        transform.parent.GetComponent<thirdPersonCamera>().isGrounded = true;
	}
}
