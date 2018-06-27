using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potCameraScript : MonoBehaviour {


    public Camera mainCamera;
    public GameObject player;
    thirdPersonCamera cameraScript;
    public List<GameObject> potSpawnerList;
	// Use this for initialization
	void Start () {
        cameraScript = player.GetComponent<thirdPersonCamera>();
        for (int i = 0; i < potSpawnerList.Count; i++)
        {
            potSpawnerList[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
    public void activatePots(){
        print("activate pots");
        //if(!cameraScript.potCam){
        //    for (int i = 0; i < potSpawnerList.Count; i++){
        //        potSpawnerList[i].SetActive(false);
        //    }
        //}
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("panSpawner");
        for (int i = 0; i < taggedObjects.Length; i++)
        {
            taggedObjects[i].GetComponent<potSpawner>().resetPos();
            taggedObjects[i].SetActive(false);
        }
            
        for (int i = 0; i < potSpawnerList.Count; i++)
        {
            potSpawnerList[i].SetActive(true);
            print(potSpawnerList[i].gameObject.name);
            potSpawnerList[i].GetComponent<potSpawner>().resetPos();
        }

    }
	void Update () {
		
	}



}
