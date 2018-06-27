using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carManagerScript : MonoBehaviour {


    public List<GameObject> carList;
    public float minSpawntime;
    public float maxSpawnTime;
    float delay;
    float currentTime;
    public bool activateSpawner = false;
	// Use this for initialization

	void Start () {
        delay = Random.Range(minSpawntime, maxSpawnTime);
	}
	
	// Update is called once per frame

    void spawnCar(){
        int carChosen = Random.Range(0, carList.Count);
        Instantiate(carList[carChosen], carList[carChosen].transform.position, Quaternion.identity);
    }
	void Update () {
        if(!activateSpawner){
            return;
        }
        if(currentTime > delay){
            spawnCar();
            currentTime = 0;
            delay = Random.Range(minSpawntime, maxSpawnTime);
        }

        currentTime += Time.deltaTime;


	}
}
