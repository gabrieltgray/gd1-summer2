using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonCamera : MonoBehaviour {
    public Camera mainCamera;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float cursorSpeed;
    private Vector3 offset;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        //offset respresents the x, y, and z coordinates of the position of the camera
        offset = new Vector3(transform.position.x-1f, transform.position.y + 1.0f, transform.position.z + 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

	private void FixedUpdate()
	{
        Vector3 movementVec = new Vector3(Input.GetAxis("Horizontal") * speed, 0f, Input.GetAxis("Vertical")*speed);
        transform.eulerAngles = new Vector3(0f, mainCamera.transform.eulerAngles.y, 0f);
        //rb.AddRelativeForce(movementVec*10f);
        rb.velocity = (transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed) + (transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        print(transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
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

        offset += (offset * (lastVelocity - rb.velocity.magnitude))/30f;

        //lerp to offset + (offset *some modifier)
        //when you release lerp back to the base offset, i.e. offset / the current modifier
        mainCamera.transform.position = transform.position + offset;
        mainCamera.transform.LookAt(transform.position);
        lastVelocity = rb.velocity.magnitude;



    }

	
}
