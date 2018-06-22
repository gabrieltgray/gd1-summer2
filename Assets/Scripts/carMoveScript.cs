using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMoveScript : MonoBehaviour {

    public List<Transform> wayPoints;
    int currentWaypoint;
	// Use this for initialization
	void Start () {
        currentWaypoint = 0;
	}
	
	// Update is called once per frame
	void Update () {
        moveCar();

	}


    void moveCar(){
        float turnStep = 1f * Time.deltaTime;
        float moveStep = 5f * Time.deltaTime;
        Vector3 targetDir = transform.position - wayPoints[currentWaypoint].transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, 
                                               targetDir, turnStep, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWaypoint].transform.position, moveStep);

    }
	private void OnTriggerEnter(Collider other)
	{
        currentWaypoint++;
        currentWaypoint = currentWaypoint % wayPoints.Count;
        print(currentWaypoint);
	}
}
