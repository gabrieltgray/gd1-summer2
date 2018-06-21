using System.Collections;
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
    private Vector3 yMovement;
    Vector3 initialOffset;
    bool potCam = false;
    bool carCam = false;
    bool defaultCam = false;
    public List<GameObject> cameraLocations;
    public float deltaRot;
    float cameraModifier = 1f;
	// Use this for initialization
	void Start () {
        isGrounded = true;
        rb = GetComponent<Rigidbody>();
        initialOffset = offset;
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
        Quaternion initRot = transform.rotation;

        Vector3 movementVec = new Vector3(Input.GetAxis("Horizontal") * speed, 0f, Input.GetAxis("Vertical")*speed);
        //transform.eulerAngles = new Vector3(0f, mainCamera.transform.eulerAngles.y, 0f);
        Vector3 targetAngle = new Vector3(0f, mainCamera.transform.eulerAngles.y, 0f);
        float directionFace = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle.y, Time.deltaTime * turnSpeed);
        transform.eulerAngles = new Vector3(0f, directionFace, 0f);
        //rb.AddRelativeForce(movementVec*10f);
        rightMovement = transform.right * Input.GetAxis("Horizontal") * speed/2f;
        frontMovement = transform.forward * Input.GetAxis("Vertical") * speed;

        deltaRot = (transform.rotation.eulerAngles- initRot.eulerAngles).y;

        print(deltaRot);

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

        offset = Vector3.Lerp(offset, initialOffset, Time.deltaTime * 5f);



        mainCamera.transform.position = transform.position + offset + transform.forward * 2f;
        mainCamera.transform.LookAt(transform.position + transform.forward*2f);


    }


	private void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "potAreaBegin" && !potCam){

            cameraModifier = 3f;
            potCam = true;
            defaultCam = false;
            carCam = false;
            initialOffset = offset * cameraModifier;
        }
        if (other.gameObject.tag == "defaultAreaBegin" && !defaultCam)
        {
            cameraModifier = 1 / cameraModifier;
            potCam = false;
            defaultCam = true;
            carCam = false;
            initialOffset = offset * cameraModifier;
        }
        if (other.gameObject.tag == "carAreaBegin" && !carCam)
        {
            cameraModifier = 1/1.5f;
            potCam = false;
            defaultCam = false;
            carCam = true;
            initialOffset = offset * cameraModifier;
        }


	}

	public void StopPeanut(){
		speed = 0;
		jumpHeight = 0;
	}

}
