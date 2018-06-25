using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour {

    public Transform[] wayPointList;
    public GameObject car;

    public Quaternion carRotation;

    public float rotateSpeed = 0.1f;

    public int currentWayPoint = 0;
    Transform targetWayPoint;

    public float speed = 5f;

	// Use this for initialization
	void Start () {
        carRotation = new Quaternion();
	}
	
	// Update is called once per frame
	void Update () {
		//check if car has anywhere to go
        if(currentWayPoint < wayPointList.Length)
        {
            if (targetWayPoint == null)
                targetWayPoint = wayPointList[currentWayPoint];
            Drive();
        }
	}

    void Drive()
    {
        /*//Flips the fucken car upside down and doesn't go to the next waypoint
        Vector3 direction = targetWayPoint.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(transform.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.time);*/

        //tbh works the best but tokyo drifts the car until the first waypoint
        Vector3 direction = targetWayPoint.position - transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.time);

        //this just fucken. spins the car in circles like it's a zamboni with dreams of ballet dancing in russia
        /*//Set the Quaternion rotation from the GameObject's position to the next GameObject's position
        carRotation.SetFromToRotation(transform.position, targetWayPoint.position);
        print("car rotation: "+ carRotation.ToString());
        //Move the GameObject towards the second GameObject
        transform.position = Vector3.Lerp(transform.position, targetWayPoint.position, rotateSpeed * Time.deltaTime);
        //Rotate the GameObject towards the second GameObject
        transform.rotation = carRotation * transform.rotation;*/

        //i n s t a n t a n e o u s  s p i n 
        /*var newRot = Quaternion.LookRotation(targetWayPoint.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, speed);*/


        /*//rotate towards the target
        //carRotation.SetFromToRotation(transform.position, targetWayPoint.position);
        //Quaternion.RotateTowards()
        car.transform.LookAt(wayPointList[currentWayPoint]);*/


        //move towards the target
        car.transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        if (transform.position == targetWayPoint.position)
        {
            currentWayPoint++;
            targetWayPoint = wayPointList[currentWayPoint];
        }
    }

}
