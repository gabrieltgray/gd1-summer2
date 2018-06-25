using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonCamera : MonoBehaviour {
    public Camera mainCamera;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float cursorSpeed;
    private Vector3 defaultOffset;
    public Vector3 offset;
    private Rigidbody rb;
    public float gravity = 9.81f;
    public float jumpHeight;
    public float turnSpeed;

    public Vector3 frontMovement;
    public Vector3 rightMovement;

    public bool isGrounded;
    private Vector3 yMovement;
    Vector3 initialOffset;
    bool potCam = false;
    bool carCam = false;
    bool defaultCam = false;
    bool goalCam = false;
    public List<GameObject> cameraLocations;
    float cameraMod =1f;
    public float distRot;
    Quaternion initRot;
	// Use this for initialization
	void Start () {
        isGrounded = true;
        rb = GetComponent<Rigidbody>();
        initialOffset = offset;
        defaultOffset = offset;
        //offset respresents the x, y, and z coordinates of the position of the camera
        //offset = new Vector3(2f, 
                             //1.6f,
                             //2f);
    }
	
	// Update is called once per frame
	void Update () {
        yMovement = new Vector3(0f, rb.velocity.y, 0f);

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            yMovement += new Vector3(0, jumpHeight, 0);

        }
	}

	private void FixedUpdate()
	{
        initRot = transform.rotation;
        Vector3 movementVec = new Vector3(Input.GetAxis("Horizontal") * speed, 0f, Input.GetAxis("Vertical")*speed);
        //transform.eulerAngles = new Vector3(0f, mainCamera.transform.eulerAngles.y, 0f);
        Vector3 targetAngle = new Vector3(0f, mainCamera.transform.eulerAngles.y, 0f);
        float directionFace = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle.y, Time.deltaTime * turnSpeed);
        transform.eulerAngles = new Vector3(0f, directionFace, 0f);
        //rb.AddRelativeForce(movementVec*10f);
        rightMovement = transform.right * Input.GetAxis("Horizontal") * speed/2f;
        frontMovement = transform.forward * Input.GetAxis("Vertical") * speed;
        distRot = (transform.rotation.eulerAngles - initRot.eulerAngles).y;





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
        //multiplying a quaternion by a vector 3 is actually just applying the rotation to the vector3

        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * cursorSpeed, Vector3.up) * offset;


        initialOffset = (Quaternion.AngleAxis(Input.GetAxis("Mouse X") * cursorSpeed, Vector3.up) * initialOffset);




        if (Input.GetKeyDown(KeyCode.L))
        {
            goalCam = !goalCam;
            offset = transform.position - GameObject.FindWithTag("Finish").transform.position;
            initialOffset = transform.position - GameObject.FindWithTag("Finish").transform.position + new Vector3(0, 10f, 0);
            if(!goalCam){
                initialOffset = transform.up + defaultOffset;
                offset = transform.up + defaultOffset;
                print("offset: " + offset);
            }
        }
       
        if(!goalCam){
            
            mainCamera.transform.position = transform.position + offset + transform.forward * 2f;
            mainCamera.transform.LookAt(transform.position + transform.forward * 2f);

        }
        else{
            
            mainCamera.transform.position = GameObject.FindWithTag("Finish").transform.position + offset + GameObject.FindWithTag("Finish").transform.forward * 2f;
            mainCamera.transform.LookAt(GameObject.FindWithTag("Finish").transform);
        }
        offset = Vector3.Lerp(offset, initialOffset, Time.deltaTime * 5f);



    }


	private void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "potAreaBegin" && !potCam){
            cameraMod = 3f;
            potCam = true;
            defaultCam = false;
            carCam = false;
            initialOffset = offset * cameraMod;
        }
        if (other.gameObject.tag == "defaultAreaBegin" && !defaultCam)
        {
            cameraMod = 1/cameraMod;
            potCam = false;
            defaultCam = true;
            carCam = false;
            initialOffset = offset * cameraMod;
        }
        if (other.gameObject.tag == "carAreaBegin" && !carCam)
        {
            cameraMod = 1 / 2f;
            potCam = false;
            defaultCam = false;
            carCam = true;
            initialOffset = offset * cameraMod;
        }
        if (other.gameObject.tag == "goalAreaBegin" && !goalCam)
        {
            cameraMod = 1 / 2f;
            potCam = false;
            defaultCam = false;
            carCam = true;
            initialOffset = offset * cameraMod;
        }

	}

	public void StopPeanut(){
		speed = 0;
		jumpHeight = 0;
	}

}
