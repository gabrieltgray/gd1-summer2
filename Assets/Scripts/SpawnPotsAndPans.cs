using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPotsAndPans : MonoBehaviour {

    //These are the variables for spawning babies
    public float spawnTimeLowerLimit;        // The minimum amount of time between each spawn
    public float spawnTimeUpperLimit;         // The maximum amount of time between each spawn
    public float spawnDelay;            // The amount of time before spawning starts.
    public GameObject[] potsAndPans;          // Array of pot and pan prefabs.

    // Use this for initialization
    void Start () {
        print("Spawner started");
        Spawn();

        Invoke("Spawn", spawnDelay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Spawn()
    {
        float spawnTime = Random.Range(spawnTimeLowerLimit, spawnTimeUpperLimit);

        int potPanIndex = Random.Range(0, potsAndPans.Length);
        Instantiate(potsAndPans[potPanIndex], transform.position, transform.rotation);

        print("spawn");
        Invoke("Spawn", spawnTime);
    }
}
