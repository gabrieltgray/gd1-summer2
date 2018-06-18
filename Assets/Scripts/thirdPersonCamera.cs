﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonCamera : MonoBehaviour {
    public Camera mainCamera;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float cursorSpeed;
    public Vector3 offset;
    private Rigidbody rb;
    public float gravity = 9.81f;
    public float jumpHeight;
    public float turnSpeed;

    public Vector3 frontMovement;
    public Vector3 rightMovement;

    public bool isGrounded;
	// Use this for initialization
	void Start () {
        isGrounded = true;
        rb = GetComponent<Rigidbody>();

        //offset respresents the x, y, and z coordinates of the position of the camera
        //offset = new Vector3(2f, 
                             //1.6f,
                             //2f);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

	private void FixedUpdate()
	{

        Vector3 movementVec = new Vector3(Input.GetAxis("Horizontal") * speed, 0f, Input.GetAxis("Vertical")*speed);
        //transform.eulerAngles = new Vector3(0f, mainCamera.transform.eulerAngles.y, 0f);
        Vector3 targetAngle = new Vector3(0f, mainCamera.transform.eulerAngles.y, 0f);
        float directionFace = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle.y, Time.deltaTime * turnSpeed);
        transform.eulerAngles = new Vector3(0f, directionFace, 0f);
        //rb.AddRelativeForce(movementVec*10f);
        rightMovement = transform.right * Input.GetAxis("Horizontal") * speed;
        frontMovement = transform.forward * Input.GetAxis("Vertical") * speed;

        print("front movement: " + frontMovement.magnitude * Mathf.Sign(frontMovement.x));
        print("right movement: " + rightMovement.magnitude * Mathf.Sign(rightMovement.x));
        Vector3 yMovement = new Vector3(0f, rb.velocity.y, 0f);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            isGrounded = false;
            yMovement += new Vector3(0, jumpHeight, 0);

        }



        if(!isGrounded){
            yMovement -= new Vector3(0, 0.5f, 0);


        }
        rb.velocity = (rightMovement + frontMovement + yMovement);
    }
	private void LateUpdate()
	{
        moveCamera();
	}

    float lastVelocity = 0;
	void moveCamera(){


        //angle axis rotates a vector3 around an axis

        //need to multiply by offset so it returns a vector3
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * cursorSpeed, Vector3.up) * offset;
        //offset += (offset * (lastVelocity - rb.velocity.magnitude))/30f;

        //lerp to offset + (offset *some modifier)
        //when you release lerp back to the base offset, i.e. offset / the current modifier
        mainCamera.transform.position = transform.position + offset;
        mainCamera.transform.LookAt(transform.position);
        lastVelocity = rb.velocity.magnitude;



    }

	
}
