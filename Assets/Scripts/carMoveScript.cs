﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMoveScript : MonoBehaviour {

    public List<Transform> wayPoints;
    int currentWaypoint;
    public Vector3 startPos;
	// Use this for initialization
	void Start () {
        currentWaypoint = 0;
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        moveCar();

	}


    void moveCar(){
        float turnStep = 8f * Time.deltaTime;
        float moveStep = 10f * Time.deltaTime;
        Vector3 targetDir = transform.position - wayPoints[currentWaypoint].transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, 
                                               targetDir, turnStep, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWaypoint].transform.position, moveStep);

    }
	private void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "carWaypoint" && other.transform.parent == transform.parent){
            currentWaypoint++;
            if(currentWaypoint == wayPoints.Count){
                Destroy(gameObject);
            }

        }

	}
}
