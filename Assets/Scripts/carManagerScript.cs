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
    float buffer;
    float buffertime;
    float lastCarToSpawn;
	// Use this for initialization

	void Start () {
        delay = Random.Range(minSpawntime, maxSpawnTime);
        buffertime = 0;
	}
	
	// Update is called once per frame

    void spawnCar(){
        if(carList.Count ==0){
            return;
        }



        bool validCar = false;

        int carChosen = Random.Range(0, carList.Count);





        print(carChosen);
        print(carList[carChosen].transform.position);
        Instantiate(carList[carChosen], carList[carChosen].transform.position, carList[carChosen].transform.rotation);
        lastCarToSpawn = carChosen;
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
        buffertime += Time.deltaTime;



	}
}
