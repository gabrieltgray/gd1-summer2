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
    private void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag != "ground"){
            transform.parent.GetComponent<thirdPersonCamera>().isGrounded = false;
        }else{
            transform.parent.GetComponent<thirdPersonCamera>().isGrounded = true;
            print("staying");
        }
	}
	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.tag == "ground")
        {
            transform.parent.GetComponent<thirdPersonCamera>().isGrounded = false;
        }



	}
	private void OnTriggerStay(Collider other)
	{
        if (other.gameObject.tag != "ground")
        {
            transform.parent.GetComponent<thirdPersonCamera>().isGrounded = false;
        }else{
            print("staying");
            transform.parent.GetComponent<thirdPersonCamera>().isGrounded = true;
        }

	}
}
