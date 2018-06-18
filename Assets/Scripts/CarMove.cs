using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour {

    public Transform[] wayPointList;
    public GameObject car;

    public int currentWayPoint = 0;
    Transform targetWayPoint;

    public float speed = 5f;

	// Use this for initialization
	void Start () {
		
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
        //rotate towards the target
        car.transform.LookAt(wayPointList[currentWayPoint]);
        //move towards the target
        car.transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        if (transform.position == targetWayPoint.position)
        {
            currentWayPoint++;
            targetWayPoint = wayPointList[currentWayPoint];
        }
    }

}
