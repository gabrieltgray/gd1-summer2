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

    public bool isGrounded = false;
    private Vector3 yMovement;
    Vector3 initialOffset;
    public bool potCam = false;
    bool carCam = false;
    bool defaultCam = false;
    bool goalCam = false;
    public List<GameObject> cameraLocations;
    float cameraMod =1f;
    public float distRot;
    Quaternion initRot;
    Vector3 initOffset = Vector3.zero;
    public bool keyDownForJump = false;
    private bool isEnd = false;
    public Transform endPointTransform;
	// Use this for initialization
	void Start () {
        isGrounded = false;
        rb = GetComponent<Rigidbody>();
        initialOffset = offset;
        defaultOffset = offset;
        //offset respresents the x, y, and z coordinates of the position of the camera
        //offset = new Vector3(2f, 
                             //1.6f,
                             //2f);
    }
	
	// Update is called once per frame

    void endScript(){
        float moveSpeed = 1f;
        isGrounded = true;
        rb.velocity = Vector3.zero;
        transform.position = Vector3.MoveTowards(transform.position, endPointTransform.position, moveSpeed);

    }
	void Update () {
        if(isEnd){
            endScript();
            return;
        }
        yMovement = new Vector3(0f, rb.velocity.y, 0f);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            keyDownForJump = true;

        }
        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            
            isGrounded = false;
            keyDownForJump = false;

            yMovement += new Vector3(0, jumpHeight, 0);

        }

	}

	private void FixedUpdate()
	{
        if (isEnd)
        {
            return;
        }
        initRot = transform.rotation;
        Vector3 movementVec = new Vector3(Input.GetAxis("Horizontal") * speed, 0f, Input.GetAxis("Vertical")*speed);
        //transform.eulerAngles = new Vector3(0f, mainCamera.transform.eulerAngles.y, 0f);
        Vector3 targetAngle = new Vector3(0f, mainCamera.transform.eulerAngles.y, 0f);
        float directionFace = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle.y, Time.deltaTime * turnSpeed);
        transform.eulerAngles = new Vector3(0f, directionFace, 0f);
        //rb.AddRelativeForce(movementVec*10f);

        rightMovement = transform.right * Input.GetAxis("Horizontal") * speed/2f;
        frontMovement = transform.forward * Input.GetAxis("Vertical") * speed;
        if (keyDownForJump)
        {
            rightMovement = Vector3.zero;
            frontMovement = Vector3.zero;
        }
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
            //pressing l brings you back to regular
            if(!goalCam){
                initialOffset = initOffset;
                offset = initOffset;

            }else{
                initOffset = offset;
                offset = transform.position - GameObject.FindWithTag("End").transform.position;
                initialOffset = transform.position - GameObject.FindWithTag("End").transform.position + new Vector3(0, 10f, 0);
            
            }

        }
       
        if(!goalCam){
            
            mainCamera.transform.position = transform.position + offset + transform.forward * 2f;
            mainCamera.transform.LookAt(transform.position + transform.forward * 2f);

        }
        else{
            
            mainCamera.transform.position = GameObject.FindWithTag("End").transform.position + offset + GameObject.FindWithTag("End").transform.forward * 2f;
            mainCamera.transform.LookAt(GameObject.FindWithTag("End").transform);
        }
        offset = Vector3.Lerp(offset, initialOffset, Time.deltaTime * 5f);



    }

    void deactivatePots(){
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("panSpawner");
        for (int i = 0; i < taggedObjects.Length; i++){
            taggedObjects[i].GetComponent<potSpawner>().resetPos();
            taggedObjects[i].SetActive(false);
        }
    }
    void deactivateCars()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("carAreaBegin");
        for (int i = 0; i < taggedObjects.Length; i++)
        {
            taggedObjects[i].GetComponent<carManagerScript>().activateSpawner = false;
        }
    }
    private void OnTriggerEnter(Collider other)
	{
        
        if(other.gameObject.tag == "potAreaBegin" && !potCam){
            cameraMod = 3f;
            potCam = true;
            defaultCam = false;
            carCam = false;
            initialOffset = offset * cameraMod;
            other.gameObject.GetComponent<potCameraScript>().activatePots();



        }
        if (other.gameObject.tag == "potAreaBegin"){
            other.gameObject.GetComponent<potCameraScript>().activatePots();
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
            other.gameObject.GetComponent<carManagerScript>().activateSpawner = true;

        }
        if (other.gameObject.tag == "goalAreaBegin" && !goalCam)
        {
            cameraMod = 1f;
            potCam = false;
            defaultCam = false;
            carCam = true;
            initialOffset = offset * cameraMod;
        }
        if(other.gameObject.tag == "End"){
            isEnd = true;
        }
        if (!potCam)
        {
            deactivatePots();
        }
        if(!carCam){
            deactivateCars();
        }

	}

	public void StopPeanut(){
		speed = 0;
		jumpHeight = 0;
	}

}
